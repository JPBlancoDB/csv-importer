using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using CsvImporter.WebApi.Services.Azure;
using CsvImporter.WebApi.Services.Azure.Wrappers;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC.Modules
{
    public static class ServicesModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<ICsvImporterService, CsvImporterService>();
            services.AddTransient<IJobsService, JobsService>();
            services.AddTransient<ICloudStorageService, AzureCloudStorageService>();
            services.AddTransient<ICloudBlobContainer, CloudBlobContainerWrapper>();
            services.AddTransient<ICloudBlockBlob, CloudBlockBlobWrapper>();
            services.AddTransient<IQueueService, ServiceBusQueueService>();
        }
    }
}