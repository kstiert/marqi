using Marqi.Display;
using Marqi.RGB;
using SixLabors.ImageSharp;
using System;
using System.Threading;

namespace Marqi
{
    class Program
    {
        static void Main(string[] args)
        {
            IDisplay display = new RGBDisplay();
            var f6x13 = display.LoadFont("fonts/6x13.bdf");

            using (var b = Image.Load("B.bmp"))
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(100);
                }
            }
        }
    }
}
