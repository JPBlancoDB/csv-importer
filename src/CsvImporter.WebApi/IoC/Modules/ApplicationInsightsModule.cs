using CsvImporter.Common.Utilities;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.WindowsServer.TelemetryChannel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC.Modules
{
    public static class ApplicationInsightsModule
    {
        public static void Load(IServiceCollection services, IConfiguration configuration)
        {
            var appInsightKey = ConfigurationUtility.GetConfiguration(configuration, "AppInsightsInstrumentationKey");    
            services.AddApplicationInsightsTelemetry(appInsightKey);
            
            // For using temp folder in Linux
            // more info: https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core-no-visualstudio#frequently-asked-questions
            var storageFolder = ConfigurationUtility.GetConfiguration(configuration, "ApplicationInsights:TempFolder");
            services.AddSingleton<ITelemetryChannel>(new ServerTelemetryChannel
            {
                StorageFolder = storageFolder
            });
        }
    }
}