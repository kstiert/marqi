using Marqi.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marqi.RGB
{
    public class RGBFontFactory : IFontFactory<RGBLedFont>
    {
        private IDictionary<int, RGBLedFont> _fonts = new Dictionary<int, RGBLedFont>();

        public RGBLedFont GetFont(int id)
        {
            return _fonts[id];
        }

        public Task LoadFont(int id, string file)
        {
            return Task.Run(() => 
            {
                _fonts[id] = new RGBLedFont(file);
            });        
        }
    }
}
