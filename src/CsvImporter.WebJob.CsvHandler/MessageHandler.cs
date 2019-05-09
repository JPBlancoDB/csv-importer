using System;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CsvImporter.WebJob.CsvHandler
{
    public class MessageHandler
    {
        private readonly IAzureCloudStorageService _azureCloudStorageService;
        private readonly ICsvParser _csvParser;
        private readonly IConfiguration _configuration;
        private readonly IServiceBusService _serviceBusService;

        public MessageHandler(IAzureCloudStorageService azureCloudStorageService, ICsvParser csvParser, IConfiguration configuration, IServiceBusService serviceBusService)
        {
            _azureCloudStorageService = azureCloudStorageService;
            _csvParser = csvParser;
            _configuration = configuration;
            _serviceBusService = serviceBusService;
        }

        public async Task Execute([ServiceBusTrigger("%ServiceBus:QueueName%")] JobDto jobDto, ILogger log)
        {
            var batchSize = _configuration["BatchSize:Items"];

            var stream = await _azureCloudStorageService.GetFileStream(jobDto);
            var productsList = _csvParser.ParseStream(stream, Convert.ToInt16(batchSize));

            foreach (var products in productsList)
            {
                await _serviceBusService.Publish(products);    
            }
        }
    }
}