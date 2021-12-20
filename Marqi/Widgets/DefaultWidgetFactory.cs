using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Timers;
using Marqi.Data.Todoist;
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

        public DefaultWidgetFactory(ILoggerFactory loggerFactory, IFontManager fontManager, GoogleCalendar googleCalendar, TodoProject todoProject, TimerCollection timerCollection)
        {
            _logger = loggerFactory.CreateLogger<DefaultWidgetFactory>();
            _loggerFactory = loggerFactory;
            _fontManager = fontManager;
            _googleCalendar = googleCalendar;
            _todoProject = todoProject;
            _timerCollection = timerCollection;
        }

        public Task<IEnumerable<IWidget>> MakeWidgets()
        {
            var widgets = new List<IWidget>();            
            var font = _fontManager.LoadFont("fonts/5x8.bdf");
            var fontSmall = _fontManager.LoadFont("fonts/4x6.bdf");
            var fontBig = _fontManager.LoadFont("fonts/6x10.bdf");
            var datetime = new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = fontSmall,
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
            var timer = new TextWidget
            {
                Color = new Color(0, 122, 255),
                Font = fontBig,
                Position = new Position { X = 0, Y = 9 }
            };
            var progress = new ProgressBarWidget
            {
                Color = new Color(0, 200, 0),
                Position = new Position { X = 0, Y = 0},
                Width = 33,
                Height = 9
            };
            widgets.Add(new ClockWidget
            {
                Color = new Color(255, 140, 0),
                Font = fontBig,
                Position = new Position { X = 35, Y = 7 },
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
            
            widgets.Add(new LineWidget { X0 = 0, Y0 = 10, X1 = 64, Y1 = 10, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 33, Y0 = 0, X1 = 33, Y1 = 7, Color = new Color(225, 255, 255) });
            widgets.Add(datetime);
            widgets.Add(name);
            widgets.Add(task);
            widgets.Add(progress);
            widgets.Add(timer);
            _ = todo.Refresh();
            _ = cal.Refresh();
            return Task.FromResult((IEnumerable<IWidget>)widgets);

        }
    }
}