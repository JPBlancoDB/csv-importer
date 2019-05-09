using System.Collections.Generic;
using System.IO;
using CsvHelper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using CsvImporter.WebJob.CsvHandler.Extensions;

namespace CsvImporter.WebJob.CsvHandler.Services
{
    public class CsvParser : ICsvParser
    {
        public IEnumerable<List<ProductDto>> ParseStream(MemoryStream memoryStream, int batchSize)
        {
            var reader = new StreamReader(memoryStream);
            var csv = new CsvReader(reader);
            return csv.GetRecords<ProductDto>().Batch(batchSize);
        }
    }
}