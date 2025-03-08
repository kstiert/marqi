using EasyCaching.SQLite;
using Marqi.Data;
using Marqi.Data.GCalendar;
using Marqi.Data.Timers;
using Marqi.Data.Todoist;
using Marqi.Data.Weather.OpenWeather;
using Marqi.Display;
using Marqi.Fonts;
using Marqi.Options;
using Marqi.RGB;
using Marqi.V1.Hubs;
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
            services.AddSignalR();
            services.AddHttpClient();

            services.AddEasyCaching(c => {
                c.UseSQLite(sql =>{
                    sql.DBConfig = new SQLiteDBOptions { FileName = "marqi.db" };
                });
            });

            services.Configure<GoogleCalendarOptions>(Configuration.GetSection("Gcal"));
            services.Configure<TodoOptions>(Configuration.GetSection("Todo"));
            services.Configure<DisplayOptions>(Configuration.GetSection("Display"));
            services.Configure<OpenWeatherOptions>(Configuration.GetSection("OpenWeather"));

            services.AddSingleton<GoogleCalendar>();
            services.AddSingleton<TodoProject>();
            services.AddSingleton<TimerCollection>();
            services.AddSingleton<OpenWeather>();

            services.AddSingleton<IFontManager, FontManager>();

            if(Configuration.GetValue("EnableWebRGB", false))
            {
                services.AddBDFFontSupport();
                services.AddWebRGBDisplay();
            }

            if(Configuration.GetValue("EnableRGB", false))
            {
                services.AddRGBFonts();   
                services.AddRGBDisplay();
            }

            services.AddSingleton<IWidgetFactory, DefaultWidgetFactory>();
            services.AddHostedService<DisplayManager>();
            services.AddHostedService<DataSourceRefreser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseFileServer();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<BufferHub>("/v1/bufferhub");
            });
        }
    }
}
