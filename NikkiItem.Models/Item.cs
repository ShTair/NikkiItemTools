using CsvHelper.Configuration;

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
    }
}
