using CsvImporter.Common.Utilities.Abstractions;
using Microsoft.Azure.Storage.Blob;

namespace CsvImporter.Common.Utilities.Wrappers
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