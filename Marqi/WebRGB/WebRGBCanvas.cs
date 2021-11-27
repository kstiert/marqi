using Marqi.Options;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Marqi.WebRGB
{
    public class WebRGBCanvas : IWebRGBCanvas
    {
        private Image<Rgba32> _screen;
        private Image<Rgba32> _buffer;

        private readonly DisplayOptions _displayOptions;

        public WebRGBCanvas(IOptions<DisplayOptions> options)
        {
            _displayOptions = options.Value;
            _screen = new Image<Rgba32>(Width, Height, Color.Black);
            _buffer = new Image<Rgba32>(Width, Height, Color.Black);
        }

        public int Width { get { return _displayOptions.Columns * _displayOptions.PixelSize; } }

        public int Height { get { return _displayOptions.Rows * _displayOptions.PixelSize; } }

        public void Swap()
        {
            var t = _buffer;
            _buffer = _screen;
            _screen = t;
        }

        public void SetPixel(int x, int y, RGB.Color color)
        {
            throw new System.NotImplementedException();
        }
    }
}