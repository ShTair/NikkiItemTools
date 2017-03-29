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

        public void OutputLog()
        {
            var it = _items.Values.Where(t => t.Id <= 89900).ToList();

            Console.WriteLine("### 集計");
            Console.WriteLine("<table>");
            Console.WriteLine($"<tr><th>レアリティ収録済み</th><td align=\"right\">{it.Count(t => !string.IsNullOrWhiteSpace(t.Rarity))}</td></tr>");
            Console.WriteLine($"<tr><th>名前収録済み</th><td align=\"right\">{it.Count(t => !string.IsNullOrWhiteSpace(t.Name))}</td></tr>");
            Console.WriteLine($"<tr><th>属性値収録済み</th><td align=\"right\">{it.Count(t => t.HasAllData)}</td></tr>");
            Console.WriteLine($"<tr><th>色収録済み</th><td align=\"right\">{it.Count(t => !string.IsNullOrWhiteSpace(t.Color))}</td></tr>");
            Console.WriteLine("</table>");
            Console.WriteLine();

            string PKind(string v)
            {
                switch (v)
                {
                    case "ヘアスタイル": return "ヘアスタイル";
                    case "ドレス": return "ドレス";
                    case "コート": return "コート";
                    case "トップス": return "トップス";
                    case "ボトムス": return "ボトムス";
                    case "靴下": return "靴下";
                    case "靴下・ガーター": return "靴下";
                    case "シューズ": return "シューズ";
                    case "ヘアアクセサリー": return "ヘアアクセサリー";
                    case "ヘアアクセ・追加": return "ヘアアクセサリー";
                    case "ヘアアクセ・ベール": return "ヘアアクセサリー";
                    case "耳飾り": return "耳飾り";
                    case "首飾り": return "首飾り";
                    case "首飾り・ストール": return "首飾り";
                    case "腕飾り・右手": return "腕飾り";
                    case "腕飾り・左手": return "腕飾り";
                    case "腕飾り・手袋両手物": return "腕飾り";
                    case "手持品・右手": return "手持品";
                    case "手持品・左手": return "手持品";
                    case "腰飾り": return "腰飾り";
                    case "特殊・落物吊物吐息": return "特殊";
                    case "特殊・敷物": return "特殊";
                    case "特殊・マスコット": return "特殊";
                    case "特殊・眼鏡": return "特殊";
                    case "特殊・肩掛け": return "特殊";
                    case "特殊・羽": return "特殊";
                    case "特殊・しっぽ": return "特殊";
                    case "特殊・タトゥ": return "特殊";
                    case "特殊・運命の箱": return "特殊";
                    case "メイク": return "メイク";
                }

                throw new Exception();
            }

            string ZKind(string v)
            {
                switch (v)
                {
                    case "ヘアスタイル": return "ヘアスタイル";
                    case "ドレス": return "ドレス";
                    case "コート": return "コート";
                    case "トップス": return "トップス";
                    case "ボトムス": return "ボトムス";
                    case "靴下": return "靴下";
                    case "シューズ": return "シューズ";
                    case "ヘアアクセサリー": return "アクセサリー";
                    case "耳飾り": return "アクセサリー";
                    case "首飾り": return "アクセサリー";
                    case "腕飾り": return "アクセサリー";
                    case "手持品": return "アクセサリー";
                    case "腰飾り": return "アクセサリー";
                    case "特殊": return "アクセサリー";
                    case "メイク": return "メイク";
                }

                throw new Exception();
            }

            int XNum(string v)
            {
                switch (v)
                {
                    case "ヘアスタイル": return 1;
                    case "ドレス": return 2;
                    case "コート": return 3;
                    case "トップス": return 4;
                    case "ボトムス": return 5;
                    case "靴下": return 6;
                    case "靴下・ガーター": return 7;
                    case "シューズ": return 8;
                    case "アクセサリー": return 9;
                    case "ヘアアクセサリー": return 10;
                    case "ヘアアクセ・追加": return 11;
                    case "ヘアアクセ・ベール": return 12;
                    case "耳飾り": return 13;
                    case "首飾り": return 14;
                    case "首飾り・ストール": return 15;
                    case "腕飾り": return 16;
                    case "腕飾り・右手": return 17;
                    case "腕飾り・左手": return 18;
                    case "腕飾り・手袋両手物": return 19;
                    case "手持品": return 20;
                    case "手持品・右手": return 21;
                    case "手持品・左手": return 22;
                    case "腰飾り": return 23;
                    case "特殊": return 24;
                    case "特殊・落物吊物吐息": return 25;
                    case "特殊・敷物": return 26;
                    case "特殊・マスコット": return 27;
                    case "特殊・眼鏡": return 28;
                    case "特殊・肩掛け": return 29;
                    case "特殊・羽": return 30;
                    case "特殊・しっぽ": return 31;
                    case "特殊・タトゥ": return 32;
                    case "特殊・運命の箱": return 33;
                    case "メイク": return 34;
                }

                throw new Exception();
            }

            Console.WriteLine("### 属性値収録済みのものの分類");
            Console.WriteLine("<table>");
            Console.WriteLine("<tr><th>大分類</th><th>中分類</th><th>小分類</th><th>収録数</th></tr>");
            foreach (var pg in it.Where(t => t.HasAllData).GroupBy(t => ZKind(PKind(t.Kind))).OrderBy(t => XNum(t.Key)))
            {
                var g = pg.GroupBy(t => t.Kind);
                Console.Write($"<tr><th rowspan=\"{g.Count()}\">{pg.Key}</th>");

                foreach (var gi in pg.GroupBy(t => PKind(t.Kind)).OrderBy(t => XNum(t.Key)))
                {
                    var gg = gi.GroupBy(t => t.Kind);
                    Console.Write($"<th rowspan=\"{gg.Count()}\">{gi.Key}</th>");

                    foreach (var gz in gg.OrderBy(t => XNum(t.Key)))
                    {
                        Console.WriteLine($"<th>{gz.Key}</th><td align=\"right\">{gz.Count()}</td></tr>");
                    }
                }
            }
            Console.WriteLine("</table>");
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
