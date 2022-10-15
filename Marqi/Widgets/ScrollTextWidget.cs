using System;

namespace Marqi.Widgets
{
    public class ScrollTextWdiget : TextWidget
    {
        public int FontWidth { get; set; }

        public double ScrollTime { get; set; }

        private double _scroll = 0;
        public double Scroll 
        { 
            get
            {
                return _scroll;
            }
            set
            {
                _scroll = value;
                Position = new Position { X = -(int)(Overflow * _scroll), Y = Position.Y };
                Dirty();
            }
        }

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

        private int Overflow { get { return FontWidth * Text?.Length - 64 ?? 0; } }

        public override bool Update(TimeSpan delta)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                if(Overflow > 0)
                {
                    Scroll += delta / TimeSpan.FromSeconds(ScrollTime);
                    if(Scroll > 1)
                    {
                        Scroll = 1;
                    }
                }
            }

            return base.Update(delta);
        }
    }
}