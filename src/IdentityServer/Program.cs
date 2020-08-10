using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using Microsoft.Extensions.Hosting;
using Serilog.Debugging;
using Serilog.Sinks.File;
using Serilog.Formatting.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog.Sinks.Elasticsearch;

namespace IdentityServer
{
    public class Program
    {
        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                                                            .SetBasePath(Directory.GetCurrentDirectory())
                                                            .AddJsonFile("appsettings.json", true, true)
                                                            .AddEnvironmentVariables()
                                                            .Build();

        public static int Main(string[] args)
        {
            var ConnectionString = Configuration.GetConnectionString(nameof(Serilog.Sinks.Elasticsearch));
            Log.Information("{ConnectionString}", ConnectionString);

            var IndexFormat = Configuration.GetSection(nameof(Serilog.Sinks.Elasticsearch))[nameof(ElasticsearchSinkOptions.IndexFormat)];
            Log.Information("{IndexFormat}", IndexFormat);

            SelfLog.Enable(Console.Error);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://119.45.37.57:9200")) // for the docker-compose implementation
                {
                    IndexFormat = "IdentityServer-{0:yyyy.MM.dd}",

                    AutoRegisterTemplate = true,
                    OverwriteTemplate = true,
                    DetectElasticsearchVersion = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    NumberOfReplicas = 1,
                    NumberOfShards = 2,
                    //BufferBaseFilename = "./buffer",
                    RegisterTemplateFailure = RegisterTemplateRecovery.FailSink,
                    FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
                    EmitEventFailure = EmitEventFailureHandling.WriteToSelfLog |
                                       EmitEventFailureHandling.WriteToFailureSink |
                                       EmitEventFailureHandling.RaiseCallback,
                    FailureSink = new FileSink("./fail-{Date}.txt", new JsonFormatter(), null, null)
                })
                .CreateLogger();

            try
            {
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