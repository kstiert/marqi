using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Display;
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
            Basic(display);
            using (var game = new MarqiGame(display))
                game.Run();
        }

        private static void Basic(IDisplay display)
        {
            var font = display.LoadFont("fonts/5x8.bdf");
            var datetime = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 15 }
            };
            var name = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 23 }
            };
            display.AddWidget(new TextWidget
            {
                Color = new Color(255, 140, 0),
                Font = font,
                Position = new Position { X = 54, Y = 7 },
                Text = "D"
            });
            display.AddWidget(new TextWidget
            {
                Color = new Color(0, 122, 255),
                Font = font,
                Position = new Position { X = 59, Y = 7 },
                Text = "S"
            });
            display.AddWidget(new TextWidget
            {
                Color = new Color(0, 122, 255),
                Font = font,
                Position = new Position { X = 0, Y = 31 },
                Text = "HELLOHELLOHELLO"
            });
            var cal = new ListTimer<GoogleCalendarEvent>(new GoogleCalendar(), 5)
            {
                Update = (e) =>
                {
                    datetime.Text = e.Start;
                    name.Text = e.Name;
                }
            };
            display.AddWidget(new LineWidget { X0 = 0, Y0 = 7, X1 = 64, Y1 = 7, Color = new Color(225, 255, 255) });
            display.AddWidget(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            display.AddWidget(new LineWidget { X0 = 52, Y0 = 0, X1 = 52, Y1 = 7, Color = new Color(225, 255, 255) });
            display.AddWidget(datetime);
            display.AddWidget(name);
            _ = cal.Refresh();
        }
    }
}
