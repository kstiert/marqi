using System;
using System.Collections.Generic;
using Marqi.Common.Fonts;

namespace Marqi.RGBServer
{
    public class RGBFontFactory : IFontFactory<RGBLedFont>
    {
        private IDictionary<int, RGBLedFont> _fonts = new Dictionary<int, RGBLedFont>();

        public RGBLedFont GetFont(int id)
        {
            return _fonts[id];
        }

        public void LoadFont(int id, string file)
        {
            Console.WriteLine($"Loading font {file}");
            _fonts[id] = new RGBLedFont(file);      
        }
    }
}
