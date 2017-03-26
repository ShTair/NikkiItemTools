using NikkiItemLoader.Models;
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
            var header = new List<string>();

            Console.Write("Loading...");
            using (var reader = File.OpenText(path))
            {
                for (int i = 0; i < 10; i++) header.Add(await reader.ReadLineAsync());

                items.LoadCsv(reader);
            }
            Console.WriteLine(" Done");

            var p = new ItemLoadParameter
            {
                IdConverter = strs => int.Parse(strs[1]),
                ItemConverter = NormalConverter,
            };

            Console.Write("ヘアスタイル...");
            p.PostProcess = item => { item.Name = item.Name.Replace("(ヘアスタイル)", ""); };
            items.Load("https://miraclenikki.gamerch.com/%E3%83%98%E3%82%A2%E3%82%B9%E3%82%BF%E3%82%A4%E3%83%AB", 0, p);
            Console.WriteLine(" Done");

            Console.Write("ドレス...");
            p.PostProcess = item =>
            {
                item.Name = item.Name.Replace("(ドレス)", "");
                switch (item.Id) { case 10398: item.Name = "絶世の美女(墨)"; break; }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%83%89%E3%83%AC%E3%82%B9", 10000, p);
            Console.WriteLine(" Done");

            Console.Write("コート...");
            p.PostProcess = item => { item.Name = item.Name.Replace("(コート)", ""); };
            items.Load("https://miraclenikki.gamerch.com/%E3%82%B3%E3%83%BC%E3%83%88", 20000, p);
            Console.WriteLine(" Done");

            Console.Write("トップス...");
            p.PostProcess = item =>
            {
                item.Name = item.Name.Replace("(トップス)", "");
                switch (item.Id)
                {
                    case 30262: item.Name = "パールレディー"; break;
                    case 30339: item.Tags = "スポーティ"; break;
                }
            };
            items.Load("https://miraclenikki.gamerch.com/%E3%83%88%E3%83%83%E3%83%97%E3%82%B9", 30000, p);
            Console.WriteLine(" Done");

            Console.Write("ボトムス...");
            p.PostProcess = item => { item.Name = item.Name.Replace("(ボトムス)", ""); };
            items.Load("https://miraclenikki.gamerch.com/%E3%83%9C%E3%83%88%E3%83%A0%E3%82%B9", 40000, p);
            Console.WriteLine(" Done");

            Console.Write("Saving...");
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                foreach (var line in header) await writer.WriteLineAsync(line);

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

            if (string.IsNullOrWhiteSpace(item.P11 + item.P12)
                || string.IsNullOrWhiteSpace(item.P21 + item.P22)
                || string.IsNullOrWhiteSpace(item.P31 + item.P32)
                || string.IsNullOrWhiteSpace(item.P41 + item.P42)
                || string.IsNullOrWhiteSpace(item.P51 + item.P52))
            {
                item.Name = "";
            }
        }

        private static void PartConverter(IList<string> strs, Item item)
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

            if (string.IsNullOrWhiteSpace(item.P11 + item.P12)
                || string.IsNullOrWhiteSpace(item.P21 + item.P22)
                || string.IsNullOrWhiteSpace(item.P31 + item.P32)
                || string.IsNullOrWhiteSpace(item.P41 + item.P42)
                || string.IsNullOrWhiteSpace(item.P51 + item.P52))
            {
                item.Name = "";
            }
        }
    }
}
