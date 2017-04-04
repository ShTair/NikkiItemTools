using CsvHelper;
using NikkiItemCounter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace NikkiItemCounter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Dictionary<int, Item> items;
            using (var reader = File.OpenText(args[0]))
            {
                for (int i = 0; i < 10; i++) reader.ReadLine();

                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<Item.Map>();
                    csv.Configuration.HasHeaderRecord = false;
                    items = csv.GetRecords<Item>().ToDictionary(t => t.Id, t => t);
                }
            }

            var it = items.Values.Where(t => t.Id <= 89900).ToList();
            var sb = new StringBuilder();

            sb.AppendLine("### 集計");
            sb.AppendLine("<table>");
            sb.AppendLine($"<tr><th>名前収録</th><td align=\"right\">{it.Count(t => !string.IsNullOrWhiteSpace(t.Name))}</td></tr>");
            sb.AppendLine($"<tr><th>属性値収録</th><td align=\"right\">{it.Count(t => t.HasAllData)}</td></tr>");
            sb.AppendLine($"<tr><th>色収録</th><td align=\"right\">{it.Count(t => !string.IsNullOrWhiteSpace(t.Color))}</td></tr>");
            sb.AppendLine("</table>");
            sb.AppendLine();

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

            sb.AppendLine("### 属性値収録済みのものの分類");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr><th>大分類</th><th>中分類</th><th>小分類</th><th>収録数</th></tr>");
            foreach (var pg in it.Where(t => t.HasAllData).GroupBy(t => ZKind(PKind(t.Kind))).OrderBy(t => XNum(t.Key)))
            {
                var g = pg.GroupBy(t => t.Kind);
                sb.Append($"<tr><th rowspan=\"{g.Count()}\">{pg.Key}</th>");

                foreach (var gi in pg.GroupBy(t => PKind(t.Kind)).OrderBy(t => XNum(t.Key)))
                {
                    var gg = gi.GroupBy(t => t.Kind);
                    sb.Append($"<th rowspan=\"{gg.Count()}\">{gi.Key}</th>");

                    foreach (var gz in gg.OrderBy(t => XNum(t.Key)))
                    {
                        sb.AppendLine($"<th>{gz.Key}</th><td align=\"right\">{gz.Count()}</td></tr>");
                    }
                }
            }
            sb.AppendLine("</table>");

            Clipboard.SetText(sb.ToString());
        }
    }
}
