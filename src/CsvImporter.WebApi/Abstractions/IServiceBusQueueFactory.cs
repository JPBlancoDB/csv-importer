using Microsoft.Azure.ServiceBus;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IServiceBusQueueFactory
    {
        IQueueClient CreateQueueClient();
        Message CreateMessage(string content);
    }
}