using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.SqlService
{
    public class MessageHandler
    {
        public async Task Execute([ServiceBusTrigger("%ServiceBus:TopicName%", "%ServiceBus:SubscriptionName%")] List<ProductDto> products, ILogger log)
        {
            Console.WriteLine(JsonConvert.SerializeObject(products));
        }
    }
}