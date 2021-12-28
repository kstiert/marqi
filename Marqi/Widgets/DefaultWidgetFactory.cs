using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Timers;
using Marqi.Data.Todoist;
using Marqi.Data.Weather;
using Marqi.Data.Weather.OpenWeather;
using Marqi.Fonts;
using Marqi.RGB;
using Microsoft.Extensions.Logging;

namespace Marqi.Widgets
{
    public class DefaultWidgetFactory : IWidgetFactory
    {
        private readonly ILogger _logger;

        private readonly ILoggerFactory _loggerFactory;

        private readonly IFontManager _fontManager;

        private readonly GoogleCalendar _googleCalendar;

        private readonly TodoProject _todoProject;

        private readonly TimerCollection _timerCollection;

        private readonly OpenWeather _openWeather;

        public DefaultWidgetFactory(ILoggerFactory loggerFactory, IFontManager fontManager, GoogleCalendar googleCalendar, TodoProject todoProject, TimerCollection timerCollection, OpenWeather openWeather)
        {
            _logger = loggerFactory.CreateLogger<DefaultWidgetFactory>();
            _loggerFactory = loggerFactory;
            _fontManager = fontManager;
            _googleCalendar = googleCalendar;
            _todoProject = todoProject;
            _timerCollection = timerCollection;
            _openWeather = openWeather; 
        }

        public async Task<IEnumerable<IWidget>> MakeWidgets()
        {
            var widgets = new List<IWidget>();            
            var fontTask =  _fontManager.LoadFont("fonts/5x8.bdf");
            var fontSmallTask = _fontManager.LoadFont("fonts/4x6.bdf");
            var fontBigTask = _fontManager.LoadFont("fonts/6x13.bdf");
            var fontBigBoldTask = _fontManager.LoadFont("fonts/6x13B.bdf");
            var font =  await fontTask;
            var fontSmall = await fontSmallTask;
            var fontBig = await fontBigTask;
            var fontBigBold = await fontBigBoldTask;
            
            var datetime = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = fontSmall,
                Position = new Position { X = 0, Y = 16 }
            };
            var temp = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = fontSmall,
                Position = new Position { X = 49, Y = 16 }
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
            var timer = new TextWidget
            {
                Color = new Color(0, 122, 255),
                Font = fontSmall,
                Position = new Position { X = 0, Y = 6 }
            };
            var progress = new ProgressBarWidget
            {
                Color = new Color(0, 200, 0),
                CompleteColor = new Color(255, 0, 0),
                Position = new Position { X = 0, Y = 7},
                Width = 32,
                Height = 3
            };
            widgets.Add(new ClockWidget
            {
                Color = new Color(255, 0, 0),
                Font = fontBigBold,
                Position = new Position { X = 33, Y = 10 },
            });
            var cal = new ListTimer<GoogleCalendarEvent>(_loggerFactory.CreateLogger<ListTimer<GoogleCalendarEvent>>(), _googleCalendar, 5)
            {
                Update = (e) =>
                {
                    datetime.Text = e?.Start ?? "";
                    name.Text = e?.Name ?? "";
                }
            };
            var todo = new ListTimer<Todo>(_loggerFactory.CreateLogger<ListTimer<Todo>>(), _todoProject, 5)
            {
                Update = (t) =>
                {
                    task.Text = t?.Name ?? "";
                }
            };
            var timers = new ListTimer<MarqiTimer>(_loggerFactory.CreateLogger<ListTimer<MarqiTimer>>(), _timerCollection, 5)
            {
                Update = (t) =>
                {
                    timer.Text = t?.Name ?? "";
                    progress.Progress = t?.Progress ?? 0;
                }
            };
            var temps = new ListTimer<WeatherReport>(_loggerFactory.CreateLogger<ListTimer<WeatherReport>>(), _openWeather, 5)
            {
                Update = (w) =>
                {
                    temp.Text = $"{w.Temperature.PadLeft(3)}°";
                }
            };
            widgets.Add(new LineWidget { X0 = 0, Y0 = 10, X1 = 64, Y1 = 10, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 48, Y0 = 16, X1 = 64, Y1 = 16, Color = new Color(225, 255, 255) });
            // vertical
            widgets.Add(new LineWidget { X0 = 32, Y0 = 0, X1 = 32, Y1 = 10, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 48, Y0 = 10, X1 = 48, Y1 = 16, Color = new Color(225, 255, 255) });
            widgets.Add(datetime);
            widgets.Add(temp);
            widgets.Add(name);
            widgets.Add(task);
            widgets.Add(progress);
            widgets.Add(timer);
            _ = todo.Refresh();
            _ = cal.Refresh();
            _ = timers.Refresh();
            _ = temps.Refresh();
            return widgets;

        }
    }
}