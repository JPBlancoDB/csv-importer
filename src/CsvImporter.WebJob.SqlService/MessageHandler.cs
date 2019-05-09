using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.SqlService.Abstractions;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.SqlService
{
    public class MessageHandler
    {
        private readonly IProductRepository _productRepository;

        public MessageHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Execute([ServiceBusTrigger("%ServiceBus:TopicName%", "%ServiceBus:SubscriptionName%")] List<ProductDto> products, ILogger log)
        {
            await _productRepository.AddRange(products);
        }
    }
}