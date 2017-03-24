using CsvHelper;
using NikkiItemLoader.Models;
using Sgml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                _items = csv.GetRecords<Item>().ToDictionary(t => t.Id, t => t);
            }
        }

        public void SaveCsv(TextWriter writer)
        {
            using (var csv = new CsvWriter(writer))
            {
                csv.Configuration.RegisterClassMap<Item.Map>();
                csv.Configuration.HasHeaderRecord = false;
                csv.WriteRecords(_items.Values);
            }
        }

        public async Task Load(ItemLoadParameter p)
        {
            using (var wc = new WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                using (var sgml = new SgmlReader { Href = p.Uri })
                {
                    var doc = new XmlDocument();
                    doc.Load(sgml);

                    foreach (XmlElement elem in doc.GetElementsByTagName("table").Cast<XmlElement>())
                    {
                        var tid = elem.GetAttribute("id");
                        if (!tid.StartsWith("ui_wikidb_table_")) continue;


                        foreach (var item in from trs in elem.GetElementsByTagName("tr").Cast<XmlElement>()
                                             let cs = trs.GetElementsByTagName("td").Cast<XmlElement>().ToList()
                                             where cs.Count != 0
                                             select cs.Select(t => t.InnerText).ToList())
                        {
                            var offset = 0;
                            if (hasPart) offset = 1;

                            var iname = item[2 + offset].Replace("（", "(").Replace("）", ")")
                                .Replace("(ヘアスタイル)", "")
                                .Replace("(ドレス)", "")
                                .Replace("(コート)", "")
                                .Replace("(トップス)", "")
                                .Replace("(ボトムス)", "")
                                .Replace("(靴下)", "");
                            switch (iname)
                            {
                                case "パールレディ": iname = "パールレディー"; break;
                            }

                            var id = int.Parse(item[1 + offset]) + bid;

                            switch (id)
                            {
                                case 50067: continue;
                                case 50185: continue;
                            }

                            var si = _items[id];
                            if (hasPart) si.Kind = item[0] + item[1];
                            else si.Kind = item[0];
                            si.Name = iname;
                            si.Rarity = item[3 + offset].Substring(1);
                            si.P11 = item[4 + offset].ToUpper();
                            si.P12 = item[5 + offset].ToUpper();
                            si.P21 = item[6 + offset].ToUpper();
                            si.P22 = item[7 + offset].ToUpper();
                            si.P31 = item[8 + offset].ToUpper();
                            si.P32 = item[9 + offset].ToUpper();
                            si.P41 = item[10 + offset].ToUpper();
                            si.P42 = item[11 + offset].ToUpper();
                            si.P51 = item[12 + offset].ToUpper();
                            si.P52 = item[13 + offset].ToUpper();
                            si.Tags = (item[14 + offset] + " " + item[15 + offset]).Trim();

                            if (string.IsNullOrWhiteSpace(si.P11 + si.P12)
                                || string.IsNullOrWhiteSpace(si.P21 + si.P22)
                                || string.IsNullOrWhiteSpace(si.P31 + si.P32)
                                || string.IsNullOrWhiteSpace(si.P41 + si.P42)
                                || string.IsNullOrWhiteSpace(si.P51 + si.P52))
                            {
                                si.Name = "";
                            }

                            switch (id)
                            {
                                case 10398:
                                    si.Name = "絶世の美女(墨)";
                                    break;
                                case 30339:
                                    si.Tags = "スポーティ";
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    class ItemLoadParameter
    {
        public string Uri { get; set; }

        public int Offset { get; set; }

        public int Count { get; set; }

        public Action<IList<string>, Item> Converter { get; set; }

        public ItemLoadParameter()
        {
            Count = 10000;
        }
    }

    class NormalItemLoadParameter : ItemLoadParameter
    {
        public NormalItemLoadParameter()
        {
            Converter = (strs, item) =>
            {

            };
        }
    }
}
