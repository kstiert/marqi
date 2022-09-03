using Marqi.Common.Display;
using Marqi.Common.Fonts;
using Microsoft.Extensions.DependencyInjection;

namespace Marqi.RGB
{
    public static class RGBExtensions
    {
        public static void AddRGBFonts(this IServiceCollection services)
        {
            /*
            services.AddSingleton<RGBFontFactory>();
            services.AddSingleton<IFontFactory<RGBLedFont>>(s => s.GetRequiredService<RGBFontFactory>());
            services.AddSingleton<IFontFactory>(s => s.GetRequiredService<RGBFontFactory>());
            */
        }

        public static void AddRGBDisplay(this IServiceCollection services)
        {
            //services.AddSingleton<IDisplay, RGBDisplay>();
        }
    }
}