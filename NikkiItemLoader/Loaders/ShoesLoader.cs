using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class ShoesLoader : NormalLoader
    {
        public override int Offset => 60000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%B7%E3%83%A5%E3%83%BC%E3%82%BA";
    }
}
