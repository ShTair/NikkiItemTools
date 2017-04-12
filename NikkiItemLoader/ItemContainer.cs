using CsvHelper;
using NikkiItemLoader.Models;
using Sgml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace NikkiItemLoader
{
    class ItemContainer
    {
        private Dictionary<int, Item> _items;

        public ItemContainer()
        {
            _items = new Dictionary<int, Item>();
        }

        public void LoadCsv(TextReader reader)
        {
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<Item.Map>();
                csv.Configuration.HasHeaderRecord = false;
                _items = csv.GetRecords<Item>().ToDictionary(t => t.Id, t => t);
            }
        }

        public void SaveCsv(TextWriter writer)
        {
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.RegisterClassMap<Item.Map>();
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(_items.Values.OrderBy(t => t.Id));
            }
        }

        public void Load(string uri, int offset, ItemLoadParameter p, string defKind)
        {
            Reset(offset, p.Count, defKind);
            var existsItems = new HashSet<Item>();
            Load(uri, offset, p, existsItems);
        }

        public void Reset(int offset, int count, string defKind)
        {
            var nonIds = Enumerable.Range(offset + 1, count);
            foreach (var id in nonIds)
            {
                var item = _items[id];
                item.Kind = defKind;
                item.Name = "";
                item.Rarity = "";
                item.P11 = "";
                item.P12 = "";
                item.P21 = "";
                item.P22 = "";
                item.P31 = "";
                item.P32 = "";
                item.P41 = "";
                item.P42 = "";
                item.P51 = "";
                item.P52 = "";
                item.Tags = "";
                item.Color = "";
            }
        }

        public void Load(string uri, int offset, ItemLoadParameter p, HashSet<Item> existsItems)
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                using (var sgml = new SgmlReader { Href = uri })
                {
                    var doc = new XmlDocument();
                    doc.Load(sgml);

                    foreach (var strs in LoadItemColumn(doc))
                    {
                        var id = p.IdConverter(strs) + offset;

                        var item = _items[id];

                        p.ItemConverter(strs, item);
                        p.PostProcess?.Invoke(item);

                        existsItems.Add(item);
                    }
                }
            }
        }

        private IEnumerable<List<string>> LoadItemColumn(XmlDocument doc)
        {
            foreach (XmlElement elem in doc.GetElementsByTagName("table").Cast<XmlElement>())
            {
                var tid = elem.GetAttribute("id");
                if (!tid.StartsWith("ui_wikidb_table_")) continue;

                foreach (var strs in from trs in elem.GetElementsByTagName("tr").Cast<XmlElement>()
                                     let cs = trs.GetElementsByTagName("td").Cast<XmlElement>().ToList()
                                     where cs.Count != 0
                                     select cs.Select(t => t.InnerText).ToList())
                {
                    yield return strs;
                }
            }
        }
    }

    class ItemLoadParameter
    {
        public int Count { get; set; } = 10000;

        public Func<IList<string>, int> IdConverter { get; set; }

        public Action<IList<string>, Item> ItemConverter { get; set; }

        public Action<Item> PostProcess { get; set; }
    }
}
