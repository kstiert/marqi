using Marqi.Common.Display;
using Marqi.Common.Fonts;
using ServiceWire.TcpIp;

namespace Marqi.RGBServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fontFactory = new RGBFontFactory();
            var display = new RGBDisplay(fontFactory);

            var rcpServer = new TcpHost(8842);
            rcpServer.AddService<IFontFactory>(fontFactory);
            rcpServer.AddService<IDisplay>(display);
            rcpServer.Open();
        }
    }
}