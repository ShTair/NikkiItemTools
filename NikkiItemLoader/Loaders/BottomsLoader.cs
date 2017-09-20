using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class BottomsLoader : NormalLoader
    {
        public override int Offset => 40000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%83%9C%E3%83%88%E3%83%A0%E3%82%B9";
    }
}
