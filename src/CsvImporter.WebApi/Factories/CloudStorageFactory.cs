using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services.Azure;
using CsvImporter.WebApi.Services.Azure.Wrappers;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace CsvImporter.WebApi.Factories
{
    public class CloudStorageFactory : ICloudStorageFactory
    {
        private readonly IConfiguration _configuration;

        public CloudStorageFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ICloudBlobContainer CreateAzureBlobContainer()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_configuration["CloudStorageConnectionString"]);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return new CloudBlobContainerWrapper(cloudBlobClient.GetContainerReference(_configuration["CloudStorage:BlobName"]));
        }
    }
}