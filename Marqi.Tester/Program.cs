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
            var date = new TextWidget
            {
                Font = display.LoadFont("fonts/5x8.bdf"),
                Position = new Position { X = 0, Y = 8 }
            };
            var time = new TextWidget
            {
                Font = display.LoadFont("fonts/5x8.bdf"),
                Position = new Position { X = 0, Y = 16 }
            };
            var name = new TextWidget
            {
                Font = display.LoadFont("fonts/5x8.bdf"),
                Position = new Position { X = 0, Y = 24 }
            };
            var cal = new GoogleCalendar
            {
                Update = (e) => 
                {
                    time.Text = e.Start;
                    name.Text = e.Name;
                }
            };
            display.AddWidget(date);
            display.AddWidget(time);
            display.AddWidget(name);
            cal.Refresh();
            using (var game = new MarqiGame(display))
                game.Run();
        }
    }
}
