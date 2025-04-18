﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Orvid.Graphics.FontSupport
{
    /// <summary>
    /// The base type for a font.
    /// </summary>
    public abstract class Font
    {
        private static Dictionary<string, Font> LoadedFonts = new Dictionary<string, Font>();

        public static Font GetFont(string name)
        {
            if (LoadedFonts.ContainsKey(name))
            {
                return LoadedFonts[name];
            }
            return null;
        }
        
        public abstract Font LoadFont(Stream s);
        public abstract FontMetrics GetFontMetrics();
        public abstract bool IsSupportedType(Font f);
        public abstract string ProviderName { get; }
        public abstract List<Font> DefaultFonts { get; }

        /// <summary>
        /// The name of the font.
        /// </summary>
        public string Name 
		{
			get { return local_Name; }
			private set { local_Name = value; }
		}
		private string local_Name;
        /// <summary>
        /// The style of the font.
        /// </summary>
        public FontStyle Style 
		{
			get { return local_Style; }
			private set { local_Style = value; }
		}
		private FontStyle local_Style;
        /// <summary>
        /// The point-size of the Font rounded to an int.
        /// </summary>
        public float Size 
		{
			get { return local_Size; }
			private set { local_Size = value; }
		}
		private float local_Size;

        /// <summary>
        /// This constructor should only be used
        /// for initializing it as a loader.
        /// </summary>
        /// <param name="b"></param>
        internal Font(bool b)
        {

        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="name">The name of the font.</param>
        /// <param name="style">The style of the font.</param>
        /// <param name="size">The point-size of the font.</param>
        public Font(string name, FontStyle style, int size)
        {
            this.Name = name;
            this.Style = style;
            this.Size = (float)size;
            LoadedFonts.Add(name, this);
        }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="name">The name of the font.</param>
        /// <param name="style">The style of the font.</param>
        /// <param name="size">The point-size of the font.</param>
        public Font(string name, FontStyle style, double size)
        {
            this.Name = name;
            this.Style = style;
            this.Size = (float)size;
            LoadedFonts.Add(name, this);
        }
        
        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="name">The name of the font.</param>
        /// <param name="style">The style of the font.</param>
        /// <param name="size">The point-size of the font.</param>
        public Font(string name, FontStyle style, float size)
        {
            this.Name = name;
            this.Style = style;
            this.Size = size;
            LoadedFonts.Add(name, this);
        }
    }
}
