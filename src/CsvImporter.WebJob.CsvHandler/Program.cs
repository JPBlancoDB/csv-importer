using System.Threading.Tasks;
using CsvImporter.Common.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CsvImporter.WebJob.CsvHandler
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new HostBuilder()    
                .ConfigureAppConfiguration(options =>
                {
                    options.AddCommandLine(args);
                    options.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    CommonConfiguration.KeyVaultConfiguration(options);
                })
                .ConfigureWebJobs((context, options) =>
                {
                    CommonConfiguration.WebJobConfiguration(options, context);
                })
                .ConfigureLogging((context, options) =>
                {
                    options.SetMinimumLevel(LogLevel.Warning);
                    options.AddConsole();
                    CommonConfiguration.ApplicationInsightsConfiguration(context, options);
                })
                .ConfigureServices((context, services) =>
                {
                    //services.AddTransient<IMessageHandler, MessageHandler>();
                })
                .UseConsoleLifetime();

            var host = builder.Build();
            
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
