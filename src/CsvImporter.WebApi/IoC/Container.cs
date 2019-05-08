using CsvImporter.Common.Utilities.IoC;
using CsvImporter.WebApi.IoC.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebApi.IoC
{
    public static class Container
    {
        public static void LoadModules(IServiceCollection services, IConfiguration configuration)
        {
            ApplicationInsightsModule.Load(services, configuration);
            FactoriesModule.Load(services);
            ServicesModule.Load(services);
            RepositoriesModule.Load(services, configuration);
            CommonModule.Load(services);
        } 
    }
}