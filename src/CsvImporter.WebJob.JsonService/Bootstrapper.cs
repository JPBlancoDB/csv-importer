using CsvImporter.Common.WebJobs.Abstractions;
using CsvImporter.WebJob.JsonService.IoC;

namespace CsvImporter.WebJob.JsonService
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeApp()
        {
            var container = new Container().Initialize();
        }
    }
}