using Marqi.Common;
using Marqi.Common.Fonts;

namespace Marqi.Test
{
    public class Thing : IThing
    {
        public void ColorThing(Color c)
        {
            Color = c;
        }

        public void FontThing(Font f)
        {
            Font = f;
        }

        public Font Font {get; private set; }

        public Color Color { get; private set;}
    }
}