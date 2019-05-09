using System.Collections.Generic;
using System.IO;
using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebJob.CsvHandler.Abstractions
{
    public interface ICsvParser
    {
        IEnumerable<List<ProductDto>> ParseStream(MemoryStream memoryStream, int batchSize);
    }
}