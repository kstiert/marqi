using EasyCaching.SQLite;
using Marqi.Data.GCalendar;
using Marqi.Data.Timers;
using Marqi.Data.Todoist;
using Marqi.Data.Weather.OpenWeather;
using Marqi.Display;
using Marqi.Fonts;
using Marqi.Options;
using Marqi.RGB;
using Marqi.Widgets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marqi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureServices((context, services) => {
            services.AddHttpClient();

            services.AddEasyCaching(c => {
                c.UseSQLite(sql =>{
                    sql.DBConfig = new SQLiteDBOptions { FileName = "marqi.db" };
                });
            });

            services.Configure<GoogleCalendarOptions>(context.Configuration.GetSection("Gcal"));
            services.Configure<TodoOptions>(context.Configuration.GetSection("Todo"));
            services.Configure<DisplayOptions>(context.Configuration.GetSection("Display"));
            services.Configure<OpenWeatherOptions>(context.Configuration.GetSection("OpenWeather"));

            services.AddSingleton<GoogleCalendar>();
            services.AddSingleton<TodoProject>();
            services.AddSingleton<TimerCollection>();
            services.AddSingleton<OpenWeather>();

            services.AddSingleton<IFontManager, FontManager>();
            //services.AddBDFFontSupport();
            //services.AddWebRGBDisplay();

            if(!string.IsNullOrEmpty(context.Configuration["EnableRGB"]))
            {
                services.AddRGBFonts();   
                services.AddRGBDisplay();
            }

            services.AddSingleton<IWidgetFactory, DefaultWidgetFactory>();
            //services.AddSingleton<IWidgetFactory, SimpleWidgets>();
            services.AddHostedService<DisplayManager>();
            //services.AddHostedService<DataSourceRefreser>();
            });

        


    }
}
