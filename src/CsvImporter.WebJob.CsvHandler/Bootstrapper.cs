using CsvImporter.Common.WebJobs.Abstractions;
using CsvImporter.WebJob.CsvHandler.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CsvImporter.WebJob.CsvHandler
{
    public class Bootstrapper : IBootstrapper
    {
        public void InitializeApp()
        {
            var container = new Container().Initialize();

            var serviceBus = container.GetService<IServiceBusService>();
            
            serviceBus.RegisterOnMessageHandlerAndReceiveMessages();
        }
    }
}