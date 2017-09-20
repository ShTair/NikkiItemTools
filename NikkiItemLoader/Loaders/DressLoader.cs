using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class DressLoader : LoaderBase
    {
        public DressLoader() : base("https://miraclenikki.gamerch.com/%E3%83%89%E3%83%AC%E3%82%B9", 10000) { }
    }
}
