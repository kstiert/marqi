using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

        public Task LoadFont(int id, string file)
        {
            return Task.Run(() => 
            {
                var f = File.OpenRead(file);
                _fonts[id] = BDFFontContainer.CreateFont(f);
            });

        }
    }
}