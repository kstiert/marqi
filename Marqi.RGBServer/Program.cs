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
            //var queue = new DisplayQueue();

            var rcpServer = new NpHost(Constants.RGB_PIPE, new ServiceWireLogger());
            rcpServer.AddService<IFontFactory>(fontFactory);
            rcpServer.AddService<IDisplay>(display);
            rcpServer.Open();

            /*
            Action<IDisplay> action;

            while(!Console.KeyAvailable)
            {
                if(queue.Queue.TryDequeue(out action))
                {
                    //display.DrawLine(0,0,32,32, new Color(255,255,255));
                    action(display);
                }
                else
                {
                    Thread.Sleep(250);
                }
            }     
            */
            Console.WriteLine("Server Open");
            Console.ReadLine();       

            rcpServer.Close();
        }
    }
}