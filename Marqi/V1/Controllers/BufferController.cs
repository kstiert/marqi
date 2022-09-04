using System.IO;
using System.Threading.Tasks;
using Marqi.WebRGB;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Marqi.V1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BufferController : ControllerBase
    {
        private readonly IWebRGBCanvas _canvas;

        private Image<Rgba32> _disabledImage;

        public BufferController(IWebRGBCanvas canvas = null)
        {
            _canvas = canvas;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (_canvas == null)
            {
                return await DisabledImage();
            }

            return new FileStreamResult(await _canvas.GetScreenStream(), "application/png");
        }

        private async Task<FileStreamResult> DisabledImage()
        {
            if(_disabledImage == null)
            {
                var fc = new FontCollection();
                var ff = fc.Add("fonts/RobotoMono-Regular.ttf");
                _disabledImage = new Image<Rgba32>(800, 50, Color.White);
                _disabledImage.Mutate(c => c.DrawText("WebRGB Display Disabled", ff.CreateFont(39, FontStyle.Regular), Color.Black, new PointF(0,0)));
            }

            var stream = new MemoryStream();
            await _disabledImage.SaveAsPngAsync(stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/png");
        }
    }
}