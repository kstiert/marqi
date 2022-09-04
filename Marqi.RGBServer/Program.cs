using System;
using System.Threading;
using Marqi.Common;
using Marqi.Common.Display;
using Marqi.Common.Fonts;
using ServiceWire.NamedPipes;

namespace Marqi.RGBServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var fontFactory = new RGBFontFactory();
            var display = new RGBDisplay(fontFactory);

            var rpcServer = new NpHost(Constants.RGB_PIPE, new ServiceWireLogger());
            rpcServer.AddService<IFontFactory>(fontFactory);
            rpcServer.AddService<IDisplay>(display);
            rpcServer.Open();

            Console.WriteLine("Server Open");
            Console.ReadLine();       

            rpcServer.Close();
        }
    }
}