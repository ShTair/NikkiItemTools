using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class TopsLoader : NormalLoader
    {
        public override int Offset => 30000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%83%88%E3%83%83%E3%83%97%E3%82%B9";
    }
}
