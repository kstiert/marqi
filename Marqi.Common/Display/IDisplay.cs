﻿using Marqi.Common.Fonts;

namespace Marqi.Common.Display
{
    public interface IDisplay
    {
        void Clear();
        void DrawLine(int x0, int y0, int x1, int y1, Color color);
        void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false);
        void Fill(Color color);
        void SetPixel(int x, int y, Color color);
        void Swap();
    }
}