namespace Marqi.Common.Fonts
{
    public interface IFontFactory<TFont> : IFontFactory
    {
        TFont GetFont(int id);
    }

    public interface IFontFactory
    {
        void LoadFont(int id, string file);
    }
}
