using Ical.Net;
using Marqi.RGB;
using SixLabors.ImageSharp;
using System;
using System.Net.Http;
using System.Threading;

namespace Marqi
{
    class Program
    {
        static void Main(string[] args)
        {
            var calResp = new HttpClient().GetAsync("https://calendar.google.com/calendar/ical/j822eaksjlh7ktnemt509pu95k%40group.calendar.google.com/private-2fcb409c2dd5a289303adff532f42a4f/basic.ics").Result;
            var cal = Calendar.Load(calResp.Content.ReadAsStreamAsync().Result);
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
