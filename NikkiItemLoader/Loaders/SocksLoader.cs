using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class SocksLoader : KindLoader
    {
        public override int Offset => 50000;

        protected override string Url => "https://miraclenikki.gamerch.com/%E9%9D%B4%E4%B8%8B";

        protected override string[] Kinds => new string[] { "靴下", "靴下・ガーター" };
    }
}
