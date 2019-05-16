using Marqi.Display;
using Marqi.RGB;
using SixLabors.ImageSharp;
using System;
using System.Threading;

namespace Marqi
{
    class Program
    {
        static void Main()
        {
            IDisplay display = new RGBDisplay();

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
