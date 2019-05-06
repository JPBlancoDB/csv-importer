using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;

namespace CsvImporter.WebApi.Services.Azure
{
    public class ServiceBusQueueService : IQueueService
    {
        public Task Publish(JobDto job)
        {
            throw new System.NotImplementedException();
        }
    }
}