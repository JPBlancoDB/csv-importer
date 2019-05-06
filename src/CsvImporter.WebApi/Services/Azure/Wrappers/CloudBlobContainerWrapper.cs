using CsvImporter.WebApi.Abstractions;
using Microsoft.Azure.Storage.Blob;

namespace CsvImporter.WebApi.Services.Azure.Wrappers
{
    public class CloudBlobContainerWrapper : ICloudBlobContainer
    {
        private readonly CloudBlobContainer _cloudBlobContainer;

        public CloudBlobContainerWrapper(CloudBlobContainer cloudBlobContainer)
        {
            _cloudBlobContainer = cloudBlobContainer;
        }

        public ICloudBlockBlob GetBlockBlobReference(string containerName)
        {
            return new CloudBlockBlobWrapper(_cloudBlobContainer.GetBlockBlobReference(containerName));
        }
    }
}