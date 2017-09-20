using System.ComponentModel.Composition;

namespace NikkiItemLoader.Loaders
{
    abstract class AccessoryLoader : LoaderBase
    {
        public AccessoryLoader(string url, string kind) : base(url, 70000, kind, "←左にアクセサリーの種類を書きます") { }
        public AccessoryLoader(string url, string[] kinds) : base(url, 70000, kinds, "←左にアクセサリーの種類を書きます") { }
    }

    [Export(typeof(LoaderBase))]
    sealed class HairAccessoryLoader : AccessoryLoader
    {
        public HairAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A0%AD", new string[] { "ヘッドアクセ", "カチューシャ", "ヴェール", "つけ耳" }) { }
    }

    [Export(typeof(LoaderBase))]
    sealed class EarAccessoryLoader : AccessoryLoader
    {
        public EarAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%80%B3", "耳飾り") { }
    }

    [Export(typeof(LoaderBase))]
    sealed class NeckAccessoryLoader : AccessoryLoader
    {
        public NeckAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E9%A6%96", new string[] { "ネックレス", "マフラー" }) { }
    }

    [Export(typeof(LoaderBase))]
    sealed class WristAccessoryLoader : AccessoryLoader
    {
        public WristAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%95", new string[] { "右手飾り", "左手飾り", "手袋" }) { }
    }

    [Export(typeof(LoaderBase))]
    sealed class BelongingsLoader : AccessoryLoader
    {
        public BelongingsLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E6%89%8B", new string[] { "右手持ち", "左手持ち", "両手持ち" }) { }
    }

    [Export(typeof(LoaderBase))]
    sealed class WaistAccessoryLoader : AccessoryLoader
    {
        public WaistAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E8%85%B0", "腰飾り") { }
    }

    [Export(typeof(LoaderBase))]
    sealed class OtherAccessoryLoader : AccessoryLoader
    {
        public OtherAccessoryLoader() : base("https://miraclenikki.gamerch.com/%E3%82%A2%E3%82%AF%E3%82%BB%E3%82%B5%E3%83%AA%E3%83%BC%E3%83%BB%E7%89%B9%E6%AE%8A", new string[] { "フェイス", "ボディ", "タトゥー", "羽根", "しっぽ", "前景", "後景", "吊り", "床", "肌" }) { }
    }
}
