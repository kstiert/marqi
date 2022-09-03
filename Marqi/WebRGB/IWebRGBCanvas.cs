using System.IO;
using System.Threading.Tasks;
using Marqi.Common.Display;

namespace Marqi.WebRGB
{
    public interface IWebRGBCanvas : IGenericCanvas
    {
        Task<Stream> GetScreenStream();
    }
}