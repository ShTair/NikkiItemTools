using CsvHelper;
using NikkiItem.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NikkiSelector
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Directory.Exists("res")) Directory.Delete("res", true);
            if (Directory.Exists("csv")) Directory.Delete("csv", true);

            var pm = new Dictionary<string, double> { { "", 0 }, { "X", 0 }, { "D", 2 }, { "C", 3 }, { "B", 4 }, { "A", 5 }, { "S", 6.5 }, { "SS", 8 }, { "SSS", 11 }, { "SSSS", 13 } };

            var vs = args[1].Split(',').Select(t => double.Parse(t)).ToList();
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

            Directory.CreateDirectory("res");
            Directory.CreateDirectory("csv");

            var gs = items.GroupBy(t => t.Kind).Where(g => !string.IsNullOrEmpty(g.Key));
            foreach (var g in gs)
            {
                var kind = g.Key;
                var kindId = Item.GetKindId(kind);
                var rate = Item.GetRate(kindId);

                IEnumerable<(double, Item)> orderdItems = g.Where(t => HasAttr(t)).Select(t => (Indexed(t).Select(p => pm[p.Item2] * vs[p.Item1]).Sum(), t)).OrderByDescending(t => t.Item1);

                var pathAll = $"res\\i_{kindId}_1_{kind}.txt";
                File.WriteAllLines(pathAll, orderdItems.Select(t => $"{t.Item1 * rate:0}\t{t.Item2.Id:00000}\t{t.Item2.Rarity}\t{t.Item2.Name},{t.Item2.Tags}\t{pm[t.Item2.P11] * vs[0] * rate}\t{pm[t.Item2.P12] * vs[1] * rate}\t{pm[t.Item2.P21] * vs[2] * rate}\t{pm[t.Item2.P22] * vs[3] * rate}\t{pm[t.Item2.P31] * vs[4] * rate}\t{pm[t.Item2.P32] * vs[5] * rate}\t{pm[t.Item2.P41] * vs[6] * rate}\t{pm[t.Item2.P42] * vs[7] * rate}\t{pm[t.Item2.P51] * vs[8] * rate}\t{pm[t.Item2.P52] * vs[9] * rate}"));

                pathAll = $"csv\\i_{kindId}_1_{kind}.csv";
                File.WriteAllLines(pathAll, orderdItems.Select(t => $"{t.Item1 * rate:0},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name},{t.Item2.Tags},{pm[t.Item2.P11] * vs[0] * rate},{pm[t.Item2.P12] * vs[1] * rate},{pm[t.Item2.P21] * vs[2] * rate},{pm[t.Item2.P22] * vs[3] * rate},{pm[t.Item2.P31] * vs[4] * rate},{pm[t.Item2.P32] * vs[5] * rate},{pm[t.Item2.P41] * vs[6] * rate},{pm[t.Item2.P42] * vs[7] * rate},{pm[t.Item2.P51] * vs[8] * rate},{pm[t.Item2.P52] * vs[9] * rate}"), Encoding.Default);

                if (ts.Count != 0)
                {
                    orderdItems = orderdItems.Where(t => t.Item2.Tags.Split(' ').Any(t2 => ts.Contains(t2)));

                    var pathTag = $"res\\i_{kindId}_0_{kind}.txt";
                    File.WriteAllLines(pathTag, orderdItems.Select(t => $"{t.Item1 * rate:0}\t{t.Item2.Id:00000}\t{t.Item2.Rarity}\t{t.Item2.Name},{t.Item2.Tags}"));

                    pathTag = $"csv\\i_{kindId}_0_{kind}.csv";
                    File.WriteAllLines(pathTag, orderdItems.Select(t => $"{t.Item1 * rate:0},{t.Item2.Id:00000},{t.Item2.Rarity},{t.Item2.Name},{t.Item2.Tags}"), Encoding.Default);
                }
            }
        }

        private static bool HasAttr(Item item)
        {
            return !string.IsNullOrEmpty(item.P11 + item.P12)
                && !string.IsNullOrEmpty(item.P21 + item.P22)
                && !string.IsNullOrEmpty(item.P31 + item.P32)
                && !string.IsNullOrEmpty(item.P41 + item.P42)
                && !string.IsNullOrEmpty(item.P51 + item.P52);
        }

        private static IEnumerable<(int, string, string)> IndexedPair(Item item)
        {
            yield return (0, item.P11, item.P12);
            yield return (1, item.P21, item.P22);
            yield return (2, item.P31, item.P32);
            yield return (3, item.P41, item.P42);
            yield return (4, item.P51, item.P52);
        }

        private static IEnumerable<(int, string)> Indexed(Item item)
        {
            yield return (0, item.P11);
            yield return (1, item.P12);
            yield return (2, item.P21);
            yield return (3, item.P22);
            yield return (4, item.P31);
            yield return (5, item.P32);
            yield return (6, item.P41);
            yield return (7, item.P42);
            yield return (8, item.P51);
            yield return (9, item.P52);
        }
    }
}
