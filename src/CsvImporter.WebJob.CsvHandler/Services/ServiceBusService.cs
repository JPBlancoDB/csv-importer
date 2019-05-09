using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Utilities.Abstractions;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.CsvHandler.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly IServiceBusFactory _serviceBusFactory;

        public ServiceBusService(IServiceBusFactory serviceBusFactory)
        {
            _serviceBusFactory = serviceBusFactory;
        }

        public async Task Publish(IEnumerable<ProductDto> products)
        {
            var topicClient = _serviceBusFactory.CreateTopicClient();

            var message = _serviceBusFactory.CreateMessage(JsonConvert.SerializeObject(products));
               
            await topicClient.SendAsync(message);
        }
    }
}