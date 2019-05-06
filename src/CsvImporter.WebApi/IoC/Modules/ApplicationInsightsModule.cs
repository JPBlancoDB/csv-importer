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
            services.AddApplicationInsightsTelemetry(configuration["AppInsightsInstrumentationKey"]);
            
            // For using temp folder in Linux
            // more info: https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core-no-visualstudio#frequently-asked-questions
            services.AddSingleton<ITelemetryChannel>(new ServerTelemetryChannel
            {
                StorageFolder = configuration["ApplicationInsights:TempFolder"]
            });
        }
    }
}