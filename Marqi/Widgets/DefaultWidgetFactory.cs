using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Data;
using Marqi.Data.GCalendar;
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

        public DefaultWidgetFactory(ILoggerFactory loggerFactory, IFontManager fontManager, GoogleCalendar googleCalendar, TodoProject todoProject)
        {
            _logger = loggerFactory.CreateLogger<DefaultWidgetFactory>();
            _loggerFactory = loggerFactory;
            _fontManager = fontManager;
            _googleCalendar = googleCalendar;
            _todoProject = todoProject;
        }

        public Task<IEnumerable<IWidget>> MakeWidgets()
        {
            var widgets = new List<IWidget>();            
            var font = _fontManager.LoadFont("fonts/5x8.bdf");
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
            widgets.Add(new ClockWidget
            {
                Color = new Color(255, 140, 0),
                Font = font,
                Position = new Position { X = 35, Y = 7 },
            });
            
            var cal = new ListTimer<GoogleCalendarEvent>(_loggerFactory.CreateLogger<ListTimer<GoogleCalendarEvent>>(), _googleCalendar, 5)
            {
                Update = (e) =>
                {
                    datetime.Text = e.Start;
                    name.Text = e.Name;
                }
            };
            var todo = new ListTimer<Todo>(_loggerFactory.CreateLogger<ListTimer<Todo>>(), _todoProject, 5)
            {
                Update = (t) =>
                {
                    task.Text = t.Name;
                }
            };
            
            widgets.Add(new LineWidget { X0 = 0, Y0 = 7, X1 = 64, Y1 = 7, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 35, Y0 = 0, X1 = 35, Y1 = 7, Color = new Color(225, 255, 255) });
            widgets.Add(datetime);
            widgets.Add(name);
            widgets.Add(task);
            _ = todo.Refresh();
            _ = cal.Refresh();
            return Task.FromResult((IEnumerable<IWidget>)widgets);

        }
    }
}