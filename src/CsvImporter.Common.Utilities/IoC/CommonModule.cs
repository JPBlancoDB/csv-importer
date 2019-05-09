using CsvImporter.Common.Utilities.Abstractions;
using CsvImporter.Common.Utilities.Factories;
using CsvImporter.Common.Utilities.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.Common.Utilities.IoC
{
    public class CommonModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<ICloudBlobContainer, CloudBlobContainerWrapper>();
            services.AddTransient<ICloudBlockBlob, CloudBlockBlobWrapper>();
            services.AddTransient<ICloudStorageFactory, CloudStorageFactory>();
            services.AddTransient<IServiceBusFactory, ServiceBusFactory>();
        }        
    }
}