using System.Collections.Generic;
using System.IO;
using Marqi.Fonts;

namespace Orvid.Graphics.FontSupport.bdf
{
    public class BDFFontFactory : IFontFactory<BDFFontContainer>
    {
        private readonly IDictionary<int, BDFFontContainer> _fonts = new Dictionary<int, BDFFontContainer>();
        
        public BDFFontContainer GetFont(int id)
        {
            return _fonts[id];
        }

        public void LoadFont(int id, string file)
        {
            var f = File.OpenRead(file);
            _fonts[id] = BDFFontContainer.CreateFont(f);
        }
    }
}