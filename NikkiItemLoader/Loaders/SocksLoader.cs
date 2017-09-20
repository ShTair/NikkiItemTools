using AngleSharp.Dom;
using NikkiItem.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class SocksLoader : NormalLoader
    {
        public override int Offset => 50000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E9%9D%B4%E4%B8%8B";

        protected override IEnumerable<Item> GetItems(IDocument doc)
        {
            var kinds = new string[] { "靴下", "靴下・ガーター" };
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
                        Kind = kinds[i],
                        Memo1 = "－",
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
