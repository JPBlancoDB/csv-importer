using CsvImporter.Common.WebJobs.Abstractions;
using CsvImporter.WebJob.SqlService.IoC;

namespace CsvImporter.WebJob.SqlService
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeApp()
        {
            var container = new Container().Initialize();
        }
    }
}