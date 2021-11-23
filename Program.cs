using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace StromMediaPlatform
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseSystemd()
             .UseSerilog((context, services, configuration) => configuration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .WriteTo.File(Path.Join(context.Configuration["LogsFolder"], "log.txt"), rollingInterval: RollingInterval.Day))
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.ConfigureKestrel(serverOptions =>
                   {
                       serverOptions.Limits.MaxRequestBodySize = 500000000; //428mb
                       serverOptions.Limits.RequestHeadersTimeout = new TimeSpan(0, 20, 0);
                       
                   });
                   webBuilder.UseStartup<Startup>();
               });
    }
}
