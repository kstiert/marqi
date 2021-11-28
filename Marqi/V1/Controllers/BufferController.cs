using System.Threading.Tasks;
using Marqi.WebRGB;
using Microsoft.AspNetCore.Mvc;

namespace Marqi.V1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class BufferController : ControllerBase
    {
        private readonly IWebRGBCanvas _canvas;

        public BufferController(IWebRGBCanvas canvas)
        {
            _canvas = canvas;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new FileStreamResult(await _canvas.GetScreenStream(), "application/png");
        }
    }
}