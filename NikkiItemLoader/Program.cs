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

            using (var reader = File.OpenText(path))
            {
                // headerを10行読み飛ばす
                for (int i = 0; i < 9; i++) await reader.ReadLineAsync();

                items.LoadCsv(reader);
            }

            File.Copy("CsvHeader.txt", path, true);

            using (var writer = new StreamWriter(path, true, new UTF8Encoding(true)))
            {
                items.SaveCsv(writer);
            }
        }
    }
}
