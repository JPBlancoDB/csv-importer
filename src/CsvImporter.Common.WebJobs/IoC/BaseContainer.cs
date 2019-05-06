using System.IO;
using CsvImporter.Common.WebJobs.Abstractions;
using CsvImporter.Common.WebJobs.Services;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.Common.WebJobs.IoC
{
    public abstract class BaseContainer : IContainer
    {
        public ServiceProvider Initialize()
        {
            var configuration = SetupConfiguration();
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddScoped<IKeyVaultService, KeyVaultServices>();
            
            LoadModules(serviceCollection);
            
            var container = serviceCollection.BuildServiceProvider();

            var keyVaultService = container.GetService<IKeyVaultService>();
            
            serviceCollection.AddApplicationInsightsTelemetry(keyVaultService.GetSecret("AppInsightsInstrumentationKey"));
            serviceCollection.AddSingleton<ITelemetryChannel>(new ServerTelemetryChannel
            {
                StorageFolder = keyVaultService.GetSecret("ApplicationInsights:TempFolder")
            });

            return container;
        }

        private static IConfigurationRoot SetupConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: true);

            return builder.Build();
        }

        public abstract void LoadModules(ServiceCollection serviceCollection);
    }
}