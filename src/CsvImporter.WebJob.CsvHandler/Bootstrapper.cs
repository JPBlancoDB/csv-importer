using CsvImporter.Common.WebJobs.Abstractions;
using CsvImporter.WebJob.CsvHandler.IoC;

namespace CsvImporter.WebJob.CsvHandler
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeApp()
        {
            var container = new Container().Initialize();
        }
    }
}