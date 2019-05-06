using System;
using System.Threading.Tasks;
using CsvImporter.WebApi.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Logging;

namespace CsvImporter.WebApi.Services
{
    public class AzureCloudStorageService : ICloudStorageService
    {
        private readonly ILogger<AzureCloudStorageService> _logger;
        private readonly ICloudStorageFactory _cloudStorageFactory;

        public AzureCloudStorageService(ILogger<AzureCloudStorageService> logger, ICloudStorageFactory cloudStorageFactory)
        {
            _logger = logger;
            _cloudStorageFactory = cloudStorageFactory;
        }

        public async Task Upload(IFormFile formFile, Guid uuid)
        {
            try
            {
                var cloudBlobContainer = _cloudStorageFactory.CreateAzureBlobContainer();

                var cloudBlockBlob = GetCloudBlockBlob(cloudBlobContainer, uuid);

                await cloudBlockBlob.UploadFromStreamAsync(formFile.OpenReadStream());
            }
            catch (StorageException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        private static CloudBlockBlob GetCloudBlockBlob(CloudBlobContainer cloudBlobContainer, Guid uuid)
        {
            var fileName = $"{uuid}.csv";
            return cloudBlobContainer.GetBlockBlobReference(fileName);
        }
    }
}