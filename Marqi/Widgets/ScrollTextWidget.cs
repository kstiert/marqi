using System;

namespace Marqi.Widgets
{
    public class ScrollTextWdiget : TextWidget
    {
        public int FontWidth { get; set; }

        public double ScrollTime { get; set; }

        public double Scroll { get; set; }

        public override string Text 
        { 
            get
            {
                return base.Text;
            }
            
            set
            {
                Scroll = 0;
                base.Text = value;
            }
        }

        public override bool Update(TimeSpan delta)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                var overflow = FontWidth * Text.Length - 64;

                if(overflow > 0)
                {
                    Scroll += delta / TimeSpan.FromSeconds(ScrollTime);
                    if(Scroll > 1)
                    {
                        Scroll = 1;
                    }

                    Position = new Position { X = -(int)(overflow * Scroll), Y = Position.Y };
                    Dirty();
                }
            }

            return base.Update(delta);
        }
    }
}