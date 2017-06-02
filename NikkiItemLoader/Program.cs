using NikkiItem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NikkiItemLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args[0]).Wait();
        }

        private static async Task Run(string path)
        {
            var items = new ItemContainer();

            Console.Write("Loading...");
            using (var reader = File.OpenText(path))
            {
                items.LoadCsv(reader);
            }
            Console.WriteLine(" Done");

            var p = new ItemLoadParameter
            {
                IdConverter = strs => int.Parse(strs[1]),
                ItemConverter = NormalConverter,
            };

            Console.Write("ヘアスタイル...");
            items.Load("https://miraclenikki.gamerch.com/%E3%83%98%E3%82%A2%E3%82%B9%E3%82%BF%E3%82%A4%E3%83%AB", 0, p, "ヘアスタイル", "－");
            Console.WriteLine(" Done");

            Console.Write("ドレス...");
            items.Load("https://miraclenikki.gamerch.com/%E3%83%89%E3%83%AC%E3%82%B9", 10000, p, "ドレス", "－");
            Console.WriteLine(" Done");

            Console.Write("コート...");
            items.Load("https://miraclenikki.gamerch.com/%E3%82%B3%E3%83%BC%E3%83%88", 20000, p, "コート", "－");
            Console.WriteLine(" Done");

            Console.Write("トップス...");
            items.Load("https://miraclenikki.gamerch.com/%E3%83%88%E3%83%83%E3%83%97%E3%82%B9", 30000, p, "トップス", "－");
            Console.WriteLine(" Done");

            Console.Write("ボトムス...");
            items.Load("https://miraclenikki.gamerch.com/%E3%83%9C%E3%83%88%E3%83%A0%E3%82%B9", 40000, p, "ボトムス", "－");
            Console.WriteLine(" Done");

            Console.Write("靴下...");
            p.IdConverter = strs => int.Parse(strs[2]);
            p.ItemConverter = PartConverter;
            p.PostProcess = item =>
            {
                item.Kind = item.Kind.Replace("+α", "・ガーター");
            };
            items.Load("https://miraclenikki.gamerch.com/%E9%9D%B4%E4%B8%8B", 50000, p, "靴下", "－");
            Console.WriteLine(" Done");

            Console.Write("シューズ...");
            p.IdConverter = strs => int.Parse(strs[1]);
            p.ItemConverter = NormalConverter;
            items.Load("https://miraclenikki.gamerch.com/%E3%82%B7%E3%83%A5%E3%83%BC%E3%82%BA", 60000, p, "シューズ", "－");
            Console.WriteLine(" Done");

            Console.WriteLine("アクセサリー");
            var ei = new HashSet<Item>();
            p.IdConverter = strs => int.Parse(strs[2]);
            p.ItemConverter = AccessoryConverter;

            Console.Write("ヘアアクセサリー...");
            p.PostProcess = item =>
            {
                switch (item.Kind)
                {
                    case "頭": item.Kind = "ヘッドアクセ"; break;
                    case "頭+1": item.Kind = "カチューシャ"; break;
                    case "頭+2": item.Kind = "ヴェール"; break;
                    case "頭+3": item.Kind = "つけ耳"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A0%AD", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("耳飾り...");
            p.PostProcess = item =>
            {
                item.Kind = item.Kind.Replace("耳", "耳飾り");
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%80%B3", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("首飾り...");
            p.PostProcess = item =>
            {
                switch (item.Kind)
                {
                    case "首+α": item.Kind = "マフラー"; break;
                    case "首": item.Kind = "ネックレス"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A6%96", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("腕飾り...");
            p.PostProcess = item =>
            {
                switch (item.Kind)
                {
                    case "右腕": item.Kind = "右手飾り"; break;
                    case "左腕": item.Kind = "左手飾り"; break;
                    case "両腕": item.Kind = "手袋"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%95", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("手持品...");
            p.PostProcess = item =>
            {
                switch (item.Kind)
                {
                    case "右手": item.Kind = "右手持ち"; break;
                    case "左手": item.Kind = "左手持ち"; break;
                    case "両手": item.Kind = "両手持ち"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E6%89%8B", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("腰飾り...");
            p.PostProcess = item =>
            {
                item.Kind = item.Kind.Replace("腰", "腰飾り");
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%B0", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("特殊...");
            p.PostProcess = item =>
            {
                switch (item.Kind)
                {
                    case "顔": item.Kind = "フェイス"; break;
                    case "肩": item.Kind = "ボディ"; break;
                    case "刺青": item.Kind = "タトゥー"; break;
                    case "背中": item.Kind = "羽根"; break;
                    case "尻尾": item.Kind = "しっぽ"; break;
                    case "前景": item.Kind = "前景"; break;
                    case "後景": item.Kind = "後景"; break;
                    case "中景": item.Kind = "吊り"; break;
                    case "地面": item.Kind = "床"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E7%89%B9%E6%AE%8A", 70000, p, ei, "←左にアクセサリーの種類を書きます");
            Console.WriteLine(" Done");

            Console.Write("メイク...");
            p.IdConverter = strs => int.Parse(strs[1]);
            p.ItemConverter = NormalConverter;
            p.PostProcess = null;
            p.Count = 9900;
            items.Load("https://miraclenikki.gamerch.com/%E3%83%A1%E3%82%A4%E3%82%AF", 80000, p, "メイク", "－");
            Console.WriteLine(" Done");

            Console.Write("Saving...");
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                items.SaveCsv(writer);
            }
            Console.WriteLine(" Done");
        }

        private static void NormalConverter(IList<string> strs, Item item)
        {
            item.Kind = strs[0];
            item.Name = strs[2].Replace("（", "(").Replace("）", ")");
            item.Rarity = strs[3].Substring(1);
            item.P11 = strs[4].ToUpper();
            item.P12 = strs[5].ToUpper();
            item.P21 = strs[6].ToUpper();
            item.P22 = strs[7].ToUpper();
            item.P31 = strs[8].ToUpper();
            item.P32 = strs[9].ToUpper();
            item.P41 = strs[10].ToUpper();
            item.P42 = strs[11].ToUpper();
            item.P51 = strs[12].ToUpper();
            item.P52 = strs[13].ToUpper();
            item.Tags = (strs[14] + " " + strs[15]).Trim();
        }

        private static void PartConverter(IList<string> strs, Item item)
        {
            item.Kind = strs[0] + strs[1];
            item.Name = strs[3].Replace("（", "(").Replace("）", ")");
            item.Rarity = strs[4].Substring(1);
            item.P11 = strs[5].ToUpper();
            item.P12 = strs[6].ToUpper();
            item.P21 = strs[7].ToUpper();
            item.P22 = strs[8].ToUpper();
            item.P31 = strs[9].ToUpper();
            item.P32 = strs[10].ToUpper();
            item.P41 = strs[11].ToUpper();
            item.P42 = strs[12].ToUpper();
            item.P51 = strs[13].ToUpper();
            item.P52 = strs[14].ToUpper();
            item.Tags = (strs[15] + " " + strs[16]).Trim();
        }

        private static void AccessoryConverter(IList<string> strs, Item item)
        {
            item.Kind = strs[1];
            item.Name = strs[3].Replace("（", "(").Replace("）", ")");
            item.Rarity = strs[4].Substring(1);
            item.P11 = strs[5].ToUpper();
            item.P12 = strs[6].ToUpper();
            item.P21 = strs[7].ToUpper();
            item.P22 = strs[8].ToUpper();
            item.P31 = strs[9].ToUpper();
            item.P32 = strs[10].ToUpper();
            item.P41 = strs[11].ToUpper();
            item.P42 = strs[12].ToUpper();
            item.P51 = strs[13].ToUpper();
            item.P52 = strs[14].ToUpper();
            item.Tags = (strs[15] + " " + strs[16]).Trim();
        }
    }
}
