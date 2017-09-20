using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class ShoesLoader : LoaderBase
    {
        public ShoesLoader() : base("https://miraclenikki.gamerch.com/%E3%82%B7%E3%83%A5%E3%83%BC%E3%82%BA", 60000) { }
    }
}
