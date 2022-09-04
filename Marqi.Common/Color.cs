
namespace Marqi.Common
{
    [Serializable]
    public class Color
    {
        public Color() {}

        public Color (int r, int g, int b)
        {
            R = (byte)r;
            G = (byte)g;
            B = (byte)b;
        }
        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
    }
}