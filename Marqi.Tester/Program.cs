using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Todoist;
using Marqi.Display;
using Marqi.RGB;
using Marqi.Widgets;
using System;
using System.Linq;

namespace Marqi.Tester
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var display = new GameDisplay(32, 64);
            using (var game = new MarqiGame(display))
                game.Run();
        }
    }
}
