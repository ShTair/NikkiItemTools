using AngleSharp;
using AngleSharp.Dom;
using NikkiItem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NikkiItemLoader.Loaders
{
    abstract class LoaderBase
    {
        private string _url;
        private string _memo;

        private Func<int, string> _kindGetter;

        public int Offset { get; }

        public virtual int Length => 10000;

        public LoaderBase(string url, int offset, string memo = "－") : this(url, offset, kindGetter: null, memo: memo) { }

        public LoaderBase(string url, int offset, string kind, string memo = "－") : this(url, offset, _ => kind, memo) { }

        public LoaderBase(string url, int offset, string[] kinds, string memo = "－") : this(url, offset, i => kinds[i], memo) { }

        public LoaderBase(string url, int offset, Func<int, string> kindGetter, string memo)
        {
            _url = url;
            Offset = offset;
            _memo = memo;
            _kindGetter = kindGetter;
        }

        public async Task<IEnumerable<Item>> LoadItems()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var doc = await BrowsingContext.New(config).OpenAsync(_url);
            return GetItems(doc);
        }

        protected virtual IEnumerable<Item> GetItems(IDocument doc)
        {
            var tables = doc.QuerySelectorAll("section#js_async_main_column_text table");
            for (int i = 0; i < tables.Length; i++)
            {
                var trs = tables[i].QuerySelectorAll("tbody tr");
                foreach (var tr in trs)
                {
                    var datas = tr.Children.Select(t => t.TextContent).ToArray();
                    var id = int.Parse(datas[1]);
                    yield return new Item
                    {
                        Id = id + Offset,
                        ItemId = id,
                        Kind = _kindGetter?.Invoke(i) ?? datas[0],
                        Memo1 = _memo,
                        Name = datas[2].Replace("（", "(").Replace("）", ")"),
                        Rarity = datas[3].Substring(1),
                        P11 = datas[4].ToUpper(),
                        P12 = datas[5].ToUpper(),
                        P21 = datas[6].ToUpper(),
                        P22 = datas[7].ToUpper(),
                        P31 = datas[8].ToUpper(),
                        P32 = datas[9].ToUpper(),
                        P41 = datas[10].ToUpper(),
                        P42 = datas[11].ToUpper(),
                        P51 = datas[12].ToUpper(),
                        P52 = datas[13].ToUpper(),
                        Tags = (datas[14] + " " + datas[15]).Trim(),
                    };
                }
            }
        }
    }
}
