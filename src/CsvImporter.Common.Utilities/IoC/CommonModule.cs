using CsvImporter.Common.Utilities.Abstractions;
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
        }        
    }
}