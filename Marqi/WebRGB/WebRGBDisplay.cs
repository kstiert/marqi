using Marqi.Display;
using Marqi.Fonts;
using Marqi.RGB;
using Orvid.Graphics.FontSupport.bdf;

namespace Marqi.WebRGB
{
    public class WebRGBDisplay : GenericDisplayBase
    {
        private readonly IWebRGBCanvas _canvas;

        public WebRGBDisplay(IFontFactory<BDFFontContainer> fontFactory, IWebRGBCanvas canvas) : base(fontFactory)
        {
            _canvas = canvas;
        }

        public override void Clear()
        {
            throw new System.NotImplementedException();
        }

        public override void Fill(Color color)
        {
            throw new System.NotImplementedException();
        }

        public override void SetPixel(int x, int y, Color color)
        {
            throw new System.NotImplementedException();
        }

        public override void Swap()
        {
            throw new System.NotImplementedException();
        }
    }
}