using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(LoaderBase))]
    sealed class MakeupLoader : LoaderBase
    {
        public override int Length => 9900;

        public MakeupLoader() : base("https://miraclenikki.gamerch.com/%E3%83%A1%E3%82%A4%E3%82%AF", 80000) { }
    }
}
