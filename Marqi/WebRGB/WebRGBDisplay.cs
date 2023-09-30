using Marqi.Display;
using Marqi.Common.Fonts;
using Orvid.Graphics.FontSupport.bdf;
using Microsoft.Extensions.Logging;

namespace Marqi.WebRGB
{
    public class WebRGBDisplay : GenericDisplayBase
    {
        private readonly IWebRGBCanvas _canvas;

        public WebRGBDisplay(ILogger<WebRGBDisplay> logger, IFontFactory<BDFFontContainer> fontFactory, IWebRGBCanvas canvas) : base(logger, fontFactory, canvas)
        {
            _canvas = canvas;
        }
    }
}