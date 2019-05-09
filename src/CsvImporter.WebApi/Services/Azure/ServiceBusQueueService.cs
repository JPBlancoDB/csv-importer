using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Utilities.Abstractions;
using CsvImporter.WebApi.Abstractions;
using Newtonsoft.Json;

namespace CsvImporter.WebApi.Services.Azure
{
    public class ServiceBusQueueService : IQueueService
    {
        private readonly IServiceBusFactory _serviceBusFactory;

        public ServiceBusQueueService(IServiceBusFactory serviceBusFactory)
        {
            _serviceBusFactory = serviceBusFactory;
        }

        public async Task Publish(JobDto job)
        {
            var client = _serviceBusFactory.CreateQueueClient();

            var message = _serviceBusFactory.CreateMessage(JsonConvert.SerializeObject(job));

            await client.SendAsync(message);
        }
    }
}