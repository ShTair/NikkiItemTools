using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class SocksLoader : LoaderBase
    {
        public SocksLoader() : base("https://miraclenikki.gamerch.com/%E9%9D%B4%E4%B8%8B", 50000, new string[] { "靴下", "靴下・ガーター" }) { }
    }
}
