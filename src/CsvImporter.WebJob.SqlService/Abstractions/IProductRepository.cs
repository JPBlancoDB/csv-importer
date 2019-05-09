using System.Collections.Generic;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebJob.SqlService.Abstractions
{
    public interface IProductRepository
    {
        Task AddRange(List<ProductDto> products);
    }
}