using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC.Modules
{
    public static class FactoriesModule
    {
        public static void Load(IServiceCollection services)
        {
            services.AddTransient<IValidationResultFactory, ValidationResultFactory>();
            services.AddTransient<IResponseFactory, ResponseFactory>();
            services.AddSingleton<IValidatorFactory, ValidatorFactory>();

            services.AddSingleton<IValidator>(x =>
            {
                var factory = x.GetRequiredService<IValidatorFactory>();
                return factory.CreateValidator();
            });

            services.AddTransient<ICloudStorageFactory, CloudStorageFactory>();
            services.AddTransient<IServiceBusQueueFactory, ServiceBusQueueFactory>();
        }
    }
}