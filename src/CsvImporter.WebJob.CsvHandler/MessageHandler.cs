using System;
using CsvImporter.Common.Contracts.DTOs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.CsvHandler
{
    public class MessageHandler
    {
        public void Execute([ServiceBusTrigger("%ServiceBus:QueueName%")] JobDto item, ILogger log)
        {
            Console.WriteLine(JsonConvert.SerializeObject(item));
        }
    }
}