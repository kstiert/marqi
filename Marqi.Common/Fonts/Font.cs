using System;

namespace Marqi.Common.Fonts
{
    [Serializable]
    public class Font
    {
        public Font(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
