using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marqi.Fonts
{
    public class FontManager
    {
        private static int _nextid = 0;

        private readonly ILogger _log;

        private readonly IEnumerable<IFontFactory> _factories;

        private readonly IDictionary<string, Font> _fonts = new Dictionary<string, Font>();
        
        public FontManager(ILogger<FontManager> log, IEnumerable<IFontFactory> factories)
        {
            _log = log;
            _factories = factories;
        }

        public IEnumerable<Font> Fonts => _fonts.Values.AsEnumerable();

        public Font LoadFont(string file)
        {
            _log.LogDebug("Loading {file}", file);

            if(_fonts.ContainsKey(file))
            {
                _log.LogDebug("Font {file} is already loaded", file);
                return _fonts[file];
            }

            var font = new Font(_nextid);
            _nextid++;

            foreach(var factory in _factories)
            {
                factory.LoadFont(font.Id, file);
            }

            _fonts[file] = font;
            _log.LogDebug("Loaded {file}", file);

            return font;
        }
    }
}
