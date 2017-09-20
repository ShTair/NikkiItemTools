using NikkiItem.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NikkiItemLoader.Loaders
{
    interface ILoader
    {
        int Offset { get; }

        int Length { get; }

        Task<IEnumerable<Item>> LoadItems();
    }
}
