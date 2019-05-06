using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using Newtonsoft.Json;

namespace CsvImporter.WebApi.Services.Azure
{
    public class ServiceBusQueueService : IQueueService
    {
        private readonly IServiceBusQueueFactory _serviceBusQueueFactory;

        public ServiceBusQueueService(IServiceBusQueueFactory serviceBusQueueFactory)
        {
            _serviceBusQueueFactory = serviceBusQueueFactory;
        }

        public async Task Publish(JobDto job)
        {
            var client = _serviceBusQueueFactory.CreateClient();

            var message = _serviceBusQueueFactory.CreateMessage(JsonConvert.SerializeObject(job));

            await client.SendAsync(message);
        }
    }
}