using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marqi.Fonts
{
    public interface IFontManager
    {
        IEnumerable<Font> Fonts { get; }

        Task<Font> LoadFont(string file);
    }
}