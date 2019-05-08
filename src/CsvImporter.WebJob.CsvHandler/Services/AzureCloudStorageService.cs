using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.CsvHandler.Abstractions;

namespace CsvImporter.WebJob.CsvHandler.Services
{
    public class AzureCloudStorageService : IAzureCloudStorageService
    {
        public object GetFile(JobDto item)
        {
            throw new System.NotImplementedException();
        }
    }
}