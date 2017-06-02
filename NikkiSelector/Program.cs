using CsvHelper;
using NikkiItem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NikkiSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            var pm = new Dictionary<string, int> { { "", 0 }, { "X", 0 }, { "D", 2 }, { "C", 3 }, { "B", 4 }, { "A", 5 }, { "S", 7 }, { "SS", 9 }, { "SSS", 11 }, { "SSSS", 13 } };

            var vs = args[1].Split(',').Select(t => int.Parse(t)).ToList();

            var tss = "";
            if (args.Length >= 3) tss = args[2];
            var ts = tss.Split(',').Where(t => !string.IsNullOrWhiteSpace(t)).ToList();

            List<Item> items;
            using (var reader = File.OpenText(args[0]))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.RegisterClassMap<Item.Map>();
                csv.Configuration.HasHeaderRecord = false;
                items = csv.GetRecords<Item>().ToList();
            }

            var gs = items.GroupBy(t => t.Kind);
            if (Directory.Exists("res")) Directory.Delete("res", true);
            Directory.CreateDirectory("res");

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
                    case "ヘッドアクセ": return 11;
                    case "ヴェール": return 12;
                    case "カチューシャ": return 13;

                    case "つけ耳": return 14;
                    case "耳飾り": return 15;

                    case "首飾り": return 16;
                    case "マフラー": return 17;
                    case "ネックレス": return 18;

                    case "腕飾り": return 19;
                    case "右手飾り": return 20;
                    case "左手飾り": return 21;
                    case "手袋": return 22;

                    case "手持品": return 23;
                    case "右手持ち": return 24;
                    case "左手持ち": return 25;
                    case "両手持ち": return 26;

                    case "腰飾り": return 27;

                    case "特殊": return 28;
                    case "フェイス": return 29;
                    case "ボディ": return 30;
                    case "タトゥー": return 31;
                    case "羽根": return 32;
                    case "しっぽ": return 33;
                    case "前景": return 34;
                    case "後景": return 35;
                    case "吊り": return 36;
                    case "床": return 37;
                    case "肌": return 38;

                    case "メイク": return 39;
                }

                throw new Exception();
            }

            foreach (var g in gs)
            {
                var name = g.Key;
                var od = XNum(name);

                IEnumerable<(int, Item)> r = g.Select(t => (pm[t.P11] * vs[0] + pm[t.P12] * vs[1] + pm[t.P21] * vs[2] + pm[t.P22] * vs[3] + pm[t.P31] * vs[4] + pm[t.P32] * vs[5] + pm[t.P41] * vs[6] + pm[t.P42] * vs[7] + pm[t.P51] * vs[8] + pm[t.P52] * vs[9], t)).OrderByDescending(t => t.Item1);

                var pi = $"res\\i_{od}_1_{name}.txt";
                File.WriteAllLines(pi, r.Select(t => $"{t.Item1:0000},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name}"));

                if (ts.Count != 0)
                {
                    r = r.Where(t => t.Item2.Tags.Split(' ').Any(t2 => ts.Contains(t2)));

                    var pt = $"res\\i_{od}_0_{name}.txt";
                    File.WriteAllLines(pt, r.Select(t => $"{t.Item1:0000},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name}"));
                }
            }
        }
    }
}
