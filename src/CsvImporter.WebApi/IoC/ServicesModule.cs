using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC
{
    public static class ServicesModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<ICsvImporterService, CsvImporterService>();
            services.AddTransient<IJobsService, JobsService>();
        }
    }
}