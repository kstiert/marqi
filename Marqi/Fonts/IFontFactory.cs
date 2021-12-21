using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marqi.Fonts
{
    public interface IFontFactory<TFont> : IFontFactory
    {
        TFont GetFont(int id);
    }

    public interface IFontFactory
    {
        Task LoadFont(int id, string file);
    }
}
