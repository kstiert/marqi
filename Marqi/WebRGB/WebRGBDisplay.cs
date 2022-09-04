using Marqi.Display;
using Marqi.Common.Fonts;
using Orvid.Graphics.FontSupport.bdf;

namespace Marqi.WebRGB
{
    public class WebRGBDisplay : GenericDisplayBase
    {
        private readonly IWebRGBCanvas _canvas;

        public WebRGBDisplay(IFontFactory<BDFFontContainer> fontFactory, IWebRGBCanvas canvas) : base(fontFactory, canvas)
        {
            _canvas = canvas;
        }
    }
}