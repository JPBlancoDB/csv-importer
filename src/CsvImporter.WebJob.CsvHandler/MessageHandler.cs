using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.CsvHandler
{
    public class MessageHandler
    {
        private readonly IAzureCloudStorageService _azureCloudStorageService;

        public MessageHandler(IAzureCloudStorageService azureCloudStorageService)
        {
            _azureCloudStorageService = azureCloudStorageService;
        }

        public void Execute([ServiceBusTrigger("%ServiceBus:QueueName%")] JobDto item, ILogger log)
        {
            Console.WriteLine(JsonConvert.SerializeObject(item));

            var csvFile = _azureCloudStorageService.GetFile(item);
            
        }
    }
}