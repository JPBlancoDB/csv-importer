using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.Common.WebJobs.Abstractions
{
    public interface IContainer
    {
        ServiceProvider Initialize();
        void LoadModules(ServiceCollection serviceCollection);
    }
}