using FaceDlib.Core.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;

namespace FaceDlib.Core.Api
{
    class Program
    {
        public static void Main(string[] args)
        {
            var pathToContentRoot = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
             .SetBasePath(pathToContentRoot)
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables();

            CoinAppSettings.CreateInstence(builder.Build());
            System.Threading.ThreadPool.SetMinThreads(100, 200);
            
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                 .ConfigureLogging(logging =>
                 {
                     logging.ClearProviders();
                     logging.SetMinimumLevel(LogLevel.Error);
                 })
                 .UseKestrel(options => {
                     options.Limits.MinRequestBodyDataRate = null;
                 })
                 .UseNLog()
                 .UseUrls(CoinAppSettings.Instance.AppSettings.ApiHost)
                 .UseIISIntegration()
                 .UseStartup<Startup>();
    }
}
