using Microsoft.Azure.ServiceBus;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IServiceBusQueueFactory
    {
        ITopicClient CreateClient();
        Message CreateMessage(string message);
    }
}