using Marqi.Widgets;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Marqi.Common.Display;

namespace Marqi.Display
{
    public class DisplayManager : BackgroundService
    {

        private readonly ILogger _logger;

        private readonly List<IWidget> _widgets = new List<IWidget>();

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
            _widgets.AddRange(await _widgetFactory.MakeWidgets());

            await base.StartAsync(cancellationToken);
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => 
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        _logger.LogTrace("Updating widgets");
                        var render = false;
                        foreach (var widget in _widgets)
                        {                       
                            if (widget.Update(TimeSpan.FromMilliseconds(500)))
                            {
                                render = true;
                            }
                        }
                        
                        if (render)
                        {
                            _logger.LogTrace("Rendering widgets");
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
                        else
                        {
                            _logger.LogTrace("Skipping render");
                        }
                    }
                    catch(Exception e)
                    {
                        _logger.LogError(e, "Display manager error");
                    }
                    Thread.Sleep(100);
                    //await Task.Delay(500, stoppingToken);
                }
            });
        }
    }
}
