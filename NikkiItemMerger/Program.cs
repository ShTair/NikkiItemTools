using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NikkiItemMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            Run(args[0]).Wait();
        }

        private static async Task Run(string path)
        {
            var header = new List<string>();

            var s1 = new Dictionary<int, string>();
            var s2 = new Dictionary<int, string>();

            Console.Write("Loading...");
            using (var reader = File.OpenText(path))
            {
                for (int i = 0; i < 10; i++)
                {
                    header.Add(await reader.ReadLineAsync());
                }

                int st = 0;
                string line;
                while (!string.IsNullOrWhiteSpace(line = await reader.ReadLineAsync()))
                {
                    switch (line[0])
                    {
                        case '<': continue;
                        case '=': st = 1; continue;
                        case '>': st = 0; continue;
                    }

                    var id = int.Parse(line.Remove(line.IndexOf(',')));

                    switch (st)
                    {
                        case 0: s1.Add(id, line); break;
                        case 1: s2.Add(id, line); break;
                    }
                }
            }
            Console.WriteLine(" Done");

            foreach (var item in s2)
            {
                var s11 = s1[item.Key].Split(',');
                var s21 = item.Value.Split(',');

                void Paste(int index)
                {
                    s11[index] = s21[index];
                }

                Paste(2);
                Paste(4);
                Paste(5);
                Paste(6);
                Paste(7);
                Paste(8);
                Paste(9);
                Paste(10);
                Paste(11);
                Paste(12);
                Paste(13);
                Paste(14);
                Paste(15);
                Paste(16);

                s1[item.Key] = string.Join(",", s11);
            }


            Console.Write("Saving...");
            using (var writer = new StreamWriter(path, false, new UTF8Encoding(true)))
            {
                foreach (var item in header)
                {
                    await writer.WriteLineAsync(item);
                }

                foreach (var item in s1.OrderBy(t => t.Key))
                {
                    await writer.WriteLineAsync(item.Value);
                }
            }
            Console.WriteLine(" Done");
        }
    }
}
