using System.Collections.Generic;
using System.Threading.Tasks;
using Marqi.Fonts;
using Marqi.Common;

namespace Marqi.Widgets
{
    public class SimpleWidgets : IWidgetFactory
    {
        private readonly IFontManager _fontManager;

        public SimpleWidgets(IFontManager fontManager)
        {
            _fontManager = fontManager;
        }

        public Task<IEnumerable<IWidget>> MakeWidgets()
        {
            var widgets = new List<IWidget>();            
            var font = _fontManager.LoadFont("fonts/5x8.bdf");
            widgets.Add(new TextWidget
            {
                Color = new Color(255, 0, 0),
                Font = font,
                Position = new Position { X = 0, Y = 16 },
                Text = "Hello World"
            });
            
            return Task.FromResult((IEnumerable<IWidget>)widgets);
        }
    }
}