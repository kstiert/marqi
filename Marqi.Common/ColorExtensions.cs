namespace Marqi.Common
{
    public static class ColorExtensions
    {
        public static Color ColorFromHex(this string hexColor)
        {
            var bytes = Convert.FromHexString(hexColor);
            return new Color(bytes[0], bytes[1], bytes[2]);
        }
    }
}