using Marqi.Data.GCalendar;
using Marqi.Widgets;
using System;

namespace Marqi.Tester
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            var display = new GameDisplay(32, 64);
            var text = new TextWidget
            {
                Font = display.LoadFont("fonts/6x13.bdf"),
                Text = "Hello World"
            };
            var cal = new GoogleCalendar
            {
                Update = (t) => text.Text = t
            };
            display.AddWidget(text);
            cal.Refresh();
            using (var game = new MarqiGame(display))
                game.Run();
        }
    }
}
