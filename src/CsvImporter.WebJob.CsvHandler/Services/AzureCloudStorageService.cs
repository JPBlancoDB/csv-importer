using System.IO;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Utilities.Abstractions;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Logging;

namespace CsvImporter.WebJob.CsvHandler.Services
{
    public class AzureCloudStorageService : IAzureCloudStorageService
    {
        private readonly ILogger<AzureCloudStorageService> _logger;
        private readonly ICloudStorageFactory _cloudStorageFactory;
        
        public AzureCloudStorageService(ILogger<AzureCloudStorageService> logger, ICloudStorageFactory cloudStorageFactory)
        {
            _logger = logger;
            _cloudStorageFactory = cloudStorageFactory;
        }
        
        public async Task<MemoryStream> GetFileStream(JobDto item)
        {
            try
            {
                var cloudBlobContainer = _cloudStorageFactory.CreateAzureBlobContainer();
                var cloudBlock = cloudBlobContainer.GetBlockBlobReference($"{item.JobId}.csv");

                var memoryStream = new MemoryStream();
                
                await cloudBlock.DownloadToStreamAsync(memoryStream);
                
                memoryStream.Seek(0, SeekOrigin.Begin);
                
                return memoryStream;
            }
            catch (StorageException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}