using CsvImporter.Common.WebJobs.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.CsvHandler.IoC
{
    public class Container : BaseContainer
    {
        public override void LoadModules(ServiceCollection serviceCollection)
        {
        }
    }
}