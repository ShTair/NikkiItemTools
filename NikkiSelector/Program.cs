using CsvHelper;
using NikkiItem.Models;
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
            // 華麗,ｼﾝﾌﾟﾙ,ｴﾚｶﾞﾝﾄ,ｱｸﾃｨﾌﾞ,大人,ｷｭｰﾄ,ｾｸｼｰ,ピュア,ｳｫｰﾑ,クール

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

            foreach (var g in gs)
            {
                var name = g.Key;
                var od = Item.GetKindId(name);
                var rr = Item.GetRate(od);

                IEnumerable<(int, Item)> r = g.Select(t => (pm[t.P11] * vs[0] + pm[t.P12] * vs[1] + pm[t.P21] * vs[2] + pm[t.P22] * vs[3] + pm[t.P31] * vs[4] + pm[t.P32] * vs[5] + pm[t.P41] * vs[6] + pm[t.P42] * vs[7] + pm[t.P51] * vs[8] + pm[t.P52] * vs[9], t)).OrderByDescending(t => t.Item1);

                var pi = $"res\\i_{od}_1_{name}.txt";
                File.WriteAllLines(pi, r.Select(t => $"{t.Item1 * od},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name}"));

                if (ts.Count != 0)
                {
                    r = r.Where(t => t.Item2.Tags.Split(' ').Any(t2 => ts.Contains(t2)));

                    var pt = $"res\\i_{od}_0_{name}.txt";
                    File.WriteAllLines(pt, r.Select(t => $"{t.Item1 * od},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name}"));
                }
            }
        }
    }
}
