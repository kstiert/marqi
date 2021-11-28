using Marqi.Display;
using Microsoft.Extensions.DependencyInjection;

namespace Marqi.WebRGB
{
    public static class WebRGBExtensions
    {
        public static void AddWebRGBDisplay(this IServiceCollection services)
        {
            services.AddSingleton<IWebRGBCanvas, WebRGBCanvas>();
            services.AddSingleton<IDisplay, WebRGBDisplay>();
        }
    }
}