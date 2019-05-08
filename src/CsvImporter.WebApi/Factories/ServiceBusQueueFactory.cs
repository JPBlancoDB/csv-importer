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

        public IQueueClient CreateQueueClient()
        {
            return new QueueClient(GetConfiguration("ServiceBusConnectionString"), GetConfiguration("ServiceBus:QueueName"));
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