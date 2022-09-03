using System.Collections.Generic;
using Marqi.Common.Fonts;

namespace Marqi.Fonts
{
    public interface IFontManager
    {
        IEnumerable<Font> Fonts { get; }

        Font LoadFont(string file);
    }
}