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

        private readonly IFontManager _fontManager;

        public DefaultWidgetFactory(ILogger<DefaultWidgetFactory> logger, IFontManager fontManager)
        {
            _logger = logger;
            _fontManager = fontManager;
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
            widgets.Add(new TextWidget
            {
                Color = new Color(255, 140, 0),
                Font = font,
                Position = new Position { X = 54, Y = 7 },
                Text = "D"
            });
            widgets.Add(new TextWidget
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
            
            widgets.Add(new LineWidget { X0 = 0, Y0 = 7, X1 = 64, Y1 = 7, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 0, Y0 = 23, X1 = 64, Y1 = 23, Color = new Color(225, 255, 255) });
            widgets.Add(new LineWidget { X0 = 52, Y0 = 0, X1 = 52, Y1 = 7, Color = new Color(225, 255, 255) });
            widgets.Add(datetime);
            widgets.Add(name);
            widgets.Add(task);
            _ = todo.Refresh();
            _ = cal.Refresh();
            return Task.FromResult((IEnumerable<IWidget>)widgets);

        }
    }
}