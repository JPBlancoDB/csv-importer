using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CsvImporter.Common.WebJobs
{
    public class CommonConfiguration
    {
        public static void KeyVaultConfiguration(IConfigurationBuilder options)
        {
            options.AddAzureKeyVault($"https://{options.Build()["KeyVault:Name"]}.vault.azure.net/");
        }
        
        public static void ApplicationInsightsConfiguration(HostBuilderContext context, ILoggingBuilder options)
        {
            var appInsightsKey = context.Configuration["AppInsightsInstrumentationKey"];
            if (!string.IsNullOrEmpty(appInsightsKey))
            {
                options.AddApplicationInsights(o => o.InstrumentationKey = appInsightsKey);
            }
        }

        public static void WebJobConfiguration(IWebJobsBuilder options, HostBuilderContext context)
        {
            options
                .AddAzureStorageCoreServices()
                .AddAzureStorage()
                .AddServiceBus(serviceBusOptions =>
                {
                    serviceBusOptions.ConnectionString = context.Configuration["ServiceBusConnectionString"];
                });
        }
    }
}