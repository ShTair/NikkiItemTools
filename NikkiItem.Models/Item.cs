using CsvHelper.Configuration;
using System;

namespace NikkiItem.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string Kind { get; set; }

        public string Memo1 { get; set; }

        public string Name { get; set; }

        public string Rarity { get; set; }

        public string P11 { get; set; }
        public string P12 { get; set; }

        public string P21 { get; set; }
        public string P22 { get; set; }

        public string P31 { get; set; }
        public string P32 { get; set; }

        public string P41 { get; set; }
        public string P42 { get; set; }

        public string P51 { get; set; }
        public string P52 { get; set; }

        public string Tags { get; set; }

        public string Color { get; set; }

        public bool HasName => !string.IsNullOrWhiteSpace(Name);

        public bool HasAllData => !string.IsNullOrWhiteSpace(P11 + P12)
                && !string.IsNullOrWhiteSpace(P21 + P22)
                && !string.IsNullOrWhiteSpace(P31 + P32)
                && !string.IsNullOrWhiteSpace(P41 + P42)
                && !string.IsNullOrWhiteSpace(P51 + P52);

        public sealed class Map : CsvClassMap<Item>
        {
            public Map()
            {
                Map(m => m.Id).Index(0);
                Map(m => m.ItemId).Index(1);
                Map(m => m.Kind).Index(2);
                Map(m => m.Memo1).Index(3);
                Map(m => m.Name).Index(4);
                Map(m => m.Rarity).Index(5);
                Map(m => m.P11).Index(6);
                Map(m => m.P12).Index(7);
                Map(m => m.P21).Index(8);
                Map(m => m.P22).Index(9);
                Map(m => m.P31).Index(10);
                Map(m => m.P32).Index(11);
                Map(m => m.P41).Index(12);
                Map(m => m.P42).Index(13);
                Map(m => m.P51).Index(14);
                Map(m => m.P52).Index(15);
                Map(m => m.Tags).Index(16);
                Map(m => m.Color).Index(17);
            }
        }

        public static int GetKindId(string kind)
        {
            switch (kind)
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

        public static double GetRate(int kindId)
        {
            switch (kindId)
            {
                case 1: return 5;

                case 2: return 20;

                case 3: return 2;

                case 4: return 10;

                case 5: return 10;

                case 6:
                case 7: return 3;

                case 8: return 4;

                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38: return 2;

                case 39: return 1;
            }

            throw new Exception();
        }
    }
}
