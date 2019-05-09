using CsvImporter.Common.Utilities.IoC;
using CsvImporter.WebJob.JsonService.Abstractions;
using CsvImporter.WebJob.JsonService.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.JsonService.IoC
{
    public class Container
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IAzureCloudStorageService, AzureCloudStorageService>();
            CommonModule.Load(services);
        }
    }
}