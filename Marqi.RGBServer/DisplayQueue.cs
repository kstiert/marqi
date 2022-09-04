using System;
using System.Collections.Concurrent;
using Marqi.Common;
using Marqi.Common.Display;
using Marqi.Common.Fonts;

namespace Marqi.RGBServer
{
    public class DisplayQueue : IDisplay
    {

        private readonly ConcurrentQueue<Action<IDisplay>> _queue = new ConcurrentQueue<Action<IDisplay>>();

        public ConcurrentQueue<Action<IDisplay>> Queue
        {
            get { return _queue; }
        }

        public void Clear()
        {
            _queue.Enqueue(d => d.Clear());
        }

        public void Fill(Color color)
        {
            _queue.Enqueue(d => d.Fill(color));
        }

        public void SetPixel(int x, int y, Color color)
        {
            _queue.Enqueue(d => d.SetPixel(x, y, color));
        }

        public void DrawLine(int x0, int y0, int x1, int y1, Color color)
        {
            _queue.Enqueue(d => d.DrawLine(x0, y0, x1, y1, color));
        }

        public void DrawText(Font font, int x, int y, Color color, string text, int spacing = 0, bool vertical = false)
        {
            _queue.Enqueue(d => d.DrawText(font, x, y, color, text, spacing, vertical));
        }

        public void Swap()
        {
            _queue.Enqueue(d => d.Swap());
        }           
    }
}
