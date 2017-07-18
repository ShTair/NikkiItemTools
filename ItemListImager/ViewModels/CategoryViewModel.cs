using System.IO;
using System.Linq;

namespace ItemListImager.ViewModels
{
    class CategoryViewModel
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string ItemsStr { get; private set; }

        public static CategoryViewModel Load(string path)
        {
            var vm = new CategoryViewModel();
            var ns = Path.GetFileNameWithoutExtension(path).Split('_');
            vm.Name = $"{ns[2]}-{ns[3]}";
            vm.Id = int.Parse(ns[1]) * 10 + int.Parse(ns[2]);

            var items = (from line in File.ReadLines(path)
                         let sp = line.Split('\t')
                         select $"{sp[3].Split(',')[0]}({sp[0]})").Take(10).ToList();

            if (items.Count == 0) return null;

            vm.ItemsStr = string.Join(", ", items);

            return vm;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}: {ItemsStr}";
        }
    }
}
