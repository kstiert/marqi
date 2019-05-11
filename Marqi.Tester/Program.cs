using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.RGB;
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
            var font = display.LoadFont("fonts/5x8.bdf");
            var datetime = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 16 }
            };
            var name = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 24 }
            };
            var cal = new ListTimer<GoogleCalendarEvent>(new GoogleCalendar(), 5)
            {
                Update = (e) => 
                {
                    datetime.Text = e.Start;
                    name.Text = e.Name;
                }
            };
            display.AddWidget(datetime);
            display.AddWidget(name);
            cal.Refresh();
            using (var game = new MarqiGame(display))
                game.Run();
        }
    }
}
