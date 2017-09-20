using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class HairstyleLoader : NormalLoader
    {
        public override int Offset => 0;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%83%98%E3%82%A2%E3%82%B9%E3%82%BF%E3%82%A4%E3%83%AB";
    }
}
