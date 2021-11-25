using Marqi.Widgets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Marqi.Display
{
    public class DisplayManager : BackgroundService
    {

        private readonly ILogger _logger;

        private readonly IList<IWidget> _widgets = new List<IWidget>();

        private readonly IEnumerable<IDisplay> _displays;

        private readonly IWidgetFactory _widgetFactory;

        public DisplayManager(ILogger<DisplayManager> logger, IEnumerable<IDisplay> displays, IWidgetFactory widgetFactory)
        {
            _logger = logger;
            _displays = displays;
            _widgetFactory = widgetFactory;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _widgets.Concat(await _widgetFactory.MakeWidgets());
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var render = false;
                foreach (var widget in _widgets)
                {
                    if (widget.Update())
                    {
                        render = true;
                    }
                }
                
                if (render)
                {
                    foreach (var widget in _widgets)
                    {
                        foreach(var display in _displays)
                        {
                            widget.Render(display);
                        }
                    }

                    foreach(var display in _displays)
                    {
                        display.Swap();
                        display.Clear();
                    }
                }
                await Task.Delay(500, stoppingToken);
            }
        }
    }
}
