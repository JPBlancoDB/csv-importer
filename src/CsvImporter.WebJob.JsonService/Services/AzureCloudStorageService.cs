using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Utilities.Abstractions;
using CsvImporter.WebJob.JsonService.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace CsvImporter.WebJob.JsonService.Services
{
    public class AzureCloudStorageService : IAzureCloudStorageService
    {
        private readonly ICloudStorageFactory _cloudStorageFactory;
        private readonly ILogger<AzureCloudStorageService> _logger;

        public AzureCloudStorageService(ICloudStorageFactory cloudStorageFactory, ILogger<AzureCloudStorageService> logger)
        {
            _cloudStorageFactory = cloudStorageFactory;
            _logger = logger;
        }

        public async Task Save(List<ProductDto> products)
        {
            try
            {
                var client = _cloudStorageFactory.CreateAzureBlobContainer();
                var cloudBlockBlob = client.GetBlockBlobReference($"{Guid.NewGuid()}.json");

                await cloudBlockBlob.UploadTextAsync(JsonConvert.SerializeObject(products));
            }
            catch (StorageException ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}