using Microsoft.Azure.ServiceBus;

namespace CsvImporter.Common.Utilities.Abstractions
{
    public interface IServiceBusFactory
    {
        IQueueClient CreateQueueClient();
        ITopicClient CreateTopicClient();
        Message CreateMessage(string content);
    }
}