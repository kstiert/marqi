using System.Collections.Generic;

namespace Marqi.Fonts
{
    public interface IFontManager
    {
        IEnumerable<Font> Fonts { get; }

        Font LoadFont(string file);
    }
}