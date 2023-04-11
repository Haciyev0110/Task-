using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Katech_Task
{
    public class Program
    {
        private static string _PathToContentRoot;

        public static void Main(string[] args)
        {

            var logger = NLog.LogManager.LoadConfiguration("NLog.config").GetCurrentClassLogger();

            try
            {
                _PathToContentRoot = Directory.GetCurrentDirectory();

                logger.Info("Init main");
                CreateHostBuilder(args).Build().Run();

            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }


        public static bool IsProduction()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return environment == Environments.Production;
        }


        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseContentRoot(_PathToContentRoot ?? Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                // Config file doesn't working in Docker without this settings
                config.SetBasePath(Directory.GetCurrentDirectory());
#if DEBUG
                    config.AddJsonFile("appsettings.Development.json", optional: false,
                    reloadOnChange: false);
#else
                                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
#endif
                    config.AddEnvironmentVariables();
                }
                )
#if DEBUG
                            .UseIISIntegration();
#else
                            .UseKestrel()
#endif                      

            }
)
.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.SetMinimumLevel(LogLevel.Trace);
})
.UseNLog(); // NLog: setup NLog for Dependency injection
        }
    }
}
