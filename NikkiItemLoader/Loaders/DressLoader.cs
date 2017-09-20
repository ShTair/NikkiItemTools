using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class DressLoader : NormalLoader
    {
        public override int Offset => 10000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%83%89%E3%83%AC%E3%82%B9";
    }
}
