using System.Net;
using Marqi.Common;
using Marqi.Common.Display;
using Marqi.Common.Fonts;
using Microsoft.Extensions.DependencyInjection;
using ServiceWire.NamedPipes;

namespace Marqi.RGB
{
    public static class RGBExtensions
    {
        public static void AddRGBFonts(this IServiceCollection services)
        { 
            services.AddSingleton<IFontFactory>(s => new NpClient<IFontFactory>(new NpEndPoint(Constants.RGB_PIPE)).Proxy);
        }

        public static void AddRGBDisplay(this IServiceCollection services)
        {
            services.AddSingleton<IDisplay>(s => new NpClient<IDisplay>(new NpEndPoint(Constants.RGB_PIPE)).Proxy);
        }
    }
}