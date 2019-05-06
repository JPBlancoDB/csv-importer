using System.Text;
using CsvImporter.Common.Utilities;
using CsvImporter.WebApi.Abstractions;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.WebApi.Factories
{
    public class ServiceBusQueueFactory : IServiceBusQueueFactory
    {
        private readonly IConfiguration _configuration;

        public ServiceBusQueueFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ITopicClient CreateClient()
        {
            return new TopicClient(GetConfiguration("ServiceBusConnectionString"), GetConfiguration("ServiceBus:TopicName"));
        }

        public Message CreateMessage(string message)
        {
            return new Message(Encoding.UTF8.GetBytes(message));
        }
        
        private string GetConfiguration(string configurationKey)
        {
            return ConfigurationUtility.GetConfiguration(_configuration, configurationKey);
        }
    }
}