using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class CoatLoader : LoaderBase
    {
        public CoatLoader() : base("https://miraclenikki.gamerch.com/%E3%82%B3%E3%83%BC%E3%83%88", 20000) { }
    }
}
