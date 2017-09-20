using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class CoatLoader : NormalLoader
    {
        public override int Offset => 20000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%B3%E3%83%BC%E3%83%88";
    }
}
