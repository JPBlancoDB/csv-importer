using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebJob.CsvHandler.Abstractions
{
    public interface IServiceBusService
    {
        Task Publish(IEnumerable<ProductDto> products);
    }
}