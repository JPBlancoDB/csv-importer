using System.Text;
using CsvImporter.Common.Utilities.Abstractions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.Common.Utilities.Factories
{
    public class ServiceBusFactory : IServiceBusFactory
    {
        private readonly IConfiguration _configuration;

        public ServiceBusFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IQueueClient CreateQueueClient()
        {
            return new QueueClient(GetConfiguration("ServiceBusConnectionString"), GetConfiguration("ServiceBus:QueueName"));
        }
        
        public ITopicClient CreateTopicClient()
        {
            return new TopicClient(GetConfiguration("ServiceBusConnectionString"), GetConfiguration("ServiceBus:TopicName"));
        }

        public Message CreateMessage(string content)
        {
            var message = new Message(Encoding.UTF8.GetBytes(content));
            message.ContentType = "application/json";
            
            return message;
        }
        
        private string GetConfiguration(string configurationKey)
        {
            return ConfigurationUtility.GetConfiguration(_configuration, configurationKey);
        }
    }
}