using CsvImporter.Common.Utilities.IoC;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using CsvImporter.WebJob.CsvHandler.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.CsvHandler.IoC
{
    public class Container
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IAzureCloudStorageService, AzureCloudStorageService>();
            services.AddTransient<ICsvParser, CsvParser>();
            services.AddTransient<IServiceBusService, ServiceBusService>();

            CommonModule.Load(services);
        }
    }
}