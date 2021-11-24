using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Todoist;
using Marqi.Display;
using Marqi.RGB;
using Marqi.Widgets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;

namespace Marqi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

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
            var task = new TextWidget
            {
                Color = new Color(0, 122, 255),
                Font = font,
                Position = new Position { X = 0, Y = 31 }
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
            var cal = new ListTimer<GoogleCalendarEvent>(new GoogleCalendar(), 5)
            {
                Update = (e) =>
                {
                    datetime.Text = e.Start;
                    name.Text = e.Name;
                }
            };
            var todo = new ListTimer<Todo>(new TodoProject(), 5)
            {
                Update = (t) =>
                {
                    task.Text = t.Name;
                }
            };
            display.AddWidget(new LineWidget { X0 = 0, Y0 = 7, X1 = 64, Y1 = 7, Color = new Color(225, 255, 255) });
            display.AddWidget(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            display.AddWidget(new LineWidget { X0 = 52, Y0 = 0, X1 = 52, Y1 = 7, Color = new Color(225, 255, 255) });
            display.AddWidget(datetime);
            display.AddWidget(name);
            display.AddWidget(task);
            _ = todo.Refresh();
            _ = cal.Refresh();
        }
    }
}
