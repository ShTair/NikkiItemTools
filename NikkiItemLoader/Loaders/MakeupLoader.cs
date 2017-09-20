using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class MakeupLoader : NormalLoader
    {
        public override int Offset => 80000;

        public override int Length => 9900;

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%83%A1%E3%82%A4%E3%82%AF";
    }
}
