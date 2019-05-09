using CsvImporter.Common.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.SqlService.IoC
{
    public class Container
    {
        public static void Load(IServiceCollection services)
        {
            CommonModule.Load(services);
        }
    }
}