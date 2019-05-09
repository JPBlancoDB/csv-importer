using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebJob.JsonService.Abstractions
{
    public interface IAzureCloudStorageService
    {
        Task Save(List<ProductDto> products);
    }
}