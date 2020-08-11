using Serilog.Debugging;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IdentityServer.Startups
{
    public static class LogExtentions
    {
        public static void ConfigureSerilog(string IndexFormat)
        {
            SelfLog.Enable(Console.Error);
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                //.WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://119.45.37.57:9200")) // for the docker-compose implementation
                {
                    IndexFormat = IndexFormat,
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
                })
                .CreateLogger();
        }

        private static IConfiguration Configuration { get; } = new ConfigurationBuilder()
                                                     .SetBasePath(Directory.GetCurrentDirectory())
                                                     .AddJsonFile("appsettings.json", true, true)
                                                     .AddEnvironmentVariables()
                                                     .Build();
    }
}
