using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    [Export(typeof(ILoader))]
    sealed class HairAccessoryLoader : KindLoader
    {
        public override int Offset => 70000;

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A0%AD";

        protected override string[] Kinds => new string[] { "ヘッドアクセ", "カチューシャ", "ヴェール", "つけ耳" };
    }

    [Export(typeof(ILoader))]
    sealed class EarAccessoryLoader : KindSetLoader
    {
        public override int Offset => 70000;

        protected override string Kind => "耳飾り";

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%80%B3";
    }

    [Export(typeof(ILoader))]
    sealed class NeckAccessoryLoader : KindLoader
    {
        public override int Offset => 70000;

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A6%96";

        protected override string[] Kinds => new string[] { "ネックレス", "マフラー" };
    }

    [Export(typeof(ILoader))]
    sealed class WristAccessoryLoader : KindLoader
    {
        public override int Offset => 70000;

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%95";

        protected override string[] Kinds => new string[] { "右手飾り", "左手飾り", "手袋" };
    }

    [Export(typeof(ILoader))]
    sealed class BelongingsLoader : KindLoader
    {
        public override int Offset => 70000;

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E6%89%8B";

        protected override string[] Kinds => new string[] { "右手持ち", "左手持ち", "両手持ち" };
    }

    [Export(typeof(ILoader))]
    sealed class WaistAccessoryLoader : KindSetLoader
    {
        public override int Offset => 70000;

        protected override string Kind => "腰飾り";

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%B0";
    }

    [Export(typeof(ILoader))]
    sealed class OtherAccessoryLoader : KindLoader
    {
        public override int Offset => 70000;

        protected override string Memo => "←左にアクセサリーの種類を書きます";

        protected override string Url => "https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E7%89%B9%E6%AE%8A";

        protected override string[] Kinds => new string[] { "フェイス", "ボディ", "タトゥー", "羽根", "しっぽ", "前景", "後景", "吊り", "床", "肌" };
    }
}
