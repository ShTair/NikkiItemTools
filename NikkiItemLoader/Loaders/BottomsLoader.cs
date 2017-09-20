using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class BottomsLoader : LoaderBase
    {
        public BottomsLoader() : base("https://miraclenikki.gamerch.com/%E3%83%9C%E3%83%88%E3%83%A0%E3%82%B9", 40000) { }
    }
}
