using Microsoft.AspNetCore.Hosting;
using Serilog;
using Microsoft.Extensions.Hosting;
using System;

namespace MvcClient
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                MyExtentions.Elasticsearch.ConfigureSerilog(nameof(MvcClient));

                Log.Information("Starting host...");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}