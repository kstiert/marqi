using Marqi.Common.Fonts;
using Microsoft.Extensions.DependencyInjection;

namespace Orvid.Graphics.FontSupport.bdf
{
    public static class BDFFontExtensions
    {
        public static void AddBDFFontSupport(this IServiceCollection services)
        {
            services.AddSingleton<BDFFontFactory>();
            services.AddSingleton<IFontFactory<BDFFontContainer>>(s => s.GetRequiredService<BDFFontFactory>());
            services.AddSingleton<IFontFactory>(s => s.GetRequiredService<BDFFontFactory>());
        }
    }
}