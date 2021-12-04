using Marqi.Data.GCalendar;
using Marqi.Data.Todoist;
using Marqi.Display;
using Marqi.Fonts;
using Marqi.Options;
using Marqi.RGB;
using Marqi.WebRGB;
using Marqi.Widgets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orvid.Graphics.FontSupport.bdf;

namespace Marqi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.Configure<GoogleCalendarOptions>(Configuration.GetSection("Gcal"));
            services.Configure<TodoOptions>(Configuration.GetSection("Todo"));
            services.Configure<DisplayOptions>(Configuration.GetSection("Display"));

            services.AddSingleton<GoogleCalendar>();
            services.AddSingleton<TodoProject>();

            services.AddSingleton<IFontManager, FontManager>();
            services.AddBDFFontSupport();
            //services.AddRGBFonts();
       
            //services.AddRGBDisplay();
            services.AddWebRGBDisplay();

            services.AddSingleton<IWidgetFactory, DefaultWidgetFactory>();
            services.AddHostedService<DisplayManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFileServer();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
