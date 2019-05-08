using CsvImporter.Common.Contracts.DTOs;

namespace CsvImporter.WebJob.CsvHandler.Abstractions
{
    public interface IAzureCloudStorageService
    {
        object GetFile(JobDto item);
    }
}