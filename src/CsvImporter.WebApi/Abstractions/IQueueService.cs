using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IQueueService
    {
        Task Publish(JobDto job);
    }
}