using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.JsonService.Abstractions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CsvImporter.WebJob.JsonService
{
    public class MessageHandler
    {
        private readonly IAzureCloudStorageService _azureCloudStorageService;

        public MessageHandler(IAzureCloudStorageService azureCloudStorageService)
        {
            _azureCloudStorageService = azureCloudStorageService;
        }

        public async Task Execute([ServiceBusTrigger("%ServiceBus:TopicName%", "%ServiceBus:SubscriptionName%")] List<ProductDto> products, ILogger log)
        {
            await _azureCloudStorageService.Save(products);
        }
    }
}