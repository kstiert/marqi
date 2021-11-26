using System.IO;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Marqi.V1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BufferController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var img = new Image<Rgba32>(100,100);
            var circle = new Star(new PointF(50, 50), 5, 25, 50);
            img.Mutate(i => i.Fill(Color.Red, circle));

            var stream = new MemoryStream();
            img.SaveAsPng(stream);
            stream.Position = 0;
            return new FileStreamResult(stream, "application/png");
        }
    }
}