using System.IO;
using System.Threading.Tasks;
using Marqi.Display;

namespace Marqi.WebRGB
{
    public interface IWebRGBCanvas : IGenericCanvas
    {
        Task<Stream> GetScreenStream();
    }
}