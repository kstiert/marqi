using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Timers;
using Marqi.Data.Todoist;
using Marqi.Data.Weather;
using Marqi.Data.Weather.OpenWeather;
using Marqi.Fonts;
using Marqi.Common;
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

        public Task<IEnumerable<IWidget>> MakeWidgets()
        {
            var widgets = new List<IWidget>();            
            var font = _fontManager.LoadFont("fonts/5x8.bdf");
            var fontSmall = _fontManager.LoadFont("fonts/4x6.bdf");
            var fontBig = _fontManager.LoadFont("fonts/6x13.bdf");
            var fontBigBold = _fontManager.LoadFont("fonts/6x13B.bdf");
            
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
                Position = new Position { X = 53, Y = 16 }
            };
            var tempIcon = new IconWidget
            {
                Position = new Position { X = 47, Y = 11 }
            };
            var name = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 23 }
            };
            var task = new ScrollTextWdiget
            {
                Color = new Color(0, 122, 255),
                Font = font,
                Position = new Position { X = 0, Y = 31 },
                ScrollTime = 8,
                FontWidth = 5
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
            var timerText = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = fontBigBold,
                Position = new Position { X = 14, Y = 10 }
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
            var todo = new ListTimer<Todo>(_loggerFactory.CreateLogger<ListTimer<Todo>>(), _todoProject, 10)
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
                    if(t != null && t.DisplayText)
                    {
                        timerText.Text = $"{(t.Remaining.Days + 1).ToString().PadLeft(3)}";
                    }
                    else
                    {
                        timerText.Text = string.Empty;
                        progress.Progress = t?.Progress ?? 0;
                    }
                }
            };
            var temps = new ListTimer<WeatherReport>(_loggerFactory.CreateLogger<ListTimer<WeatherReport>>(), _openWeather, 5)
            {
                Update = (w) =>
                {
                    temp.Text = $"{w.Temperature.PadLeft(2)}°";
                    tempIcon.Icon = w.Icon;
                }
            };
            widgets.Add(new LineWidget { X0 = 0, Y0 = 10, X1 = 64, Y1 = 10, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 46, Y0 = 16, X1 = 64, Y1 = 16, Color = new Color(225, 255, 255) });
            // vertical
            widgets.Add(new LineWidget { X0 = 32, Y0 = 0, X1 = 32, Y1 = 10, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 46, Y0 = 10, X1 = 46, Y1 = 16, Color = new Color(225, 255, 255) });
            widgets.Add(datetime);
            widgets.Add(temp);
            widgets.Add(tempIcon);
            widgets.Add(name);
            widgets.Add(task);
            widgets.Add(progress);
            widgets.Add(timerText);
            widgets.Add(timer);
            _ = todo.Refresh();
            _ = cal.Refresh();
            _ = timers.Refresh();
            _ = temps.Refresh();
            return Task.FromResult((IEnumerable<IWidget>)widgets);

        }
    }
}