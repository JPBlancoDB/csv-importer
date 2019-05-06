using System.Text;
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
            return new TopicClient(_configuration["ServiceBusConnectionString"], _configuration["ServiceBus:TopicName"]);
        }

        public Message CreateMessage(string message)
        {
            return new Message(Encoding.UTF8.GetBytes(message));
        }
    }
}