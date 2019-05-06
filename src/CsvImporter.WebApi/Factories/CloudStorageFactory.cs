using CsvImporter.WebApi.Abstractions;
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

        public CloudBlobContainer CreateAzureBlobContainer()
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(_configuration["CloudStorageConnectionString"]);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();

            return cloudBlobClient.GetContainerReference(_configuration["CloudStorage:BlobName"]);
        }
    }
}