using System.IO;
using System.Threading.Tasks;
using CsvImporter.WebApi.Abstractions;
using Microsoft.Azure.Storage.Blob;

namespace CsvImporter.WebApi.Services.Azure.Wrappers
{
    public class CloudBlockBlobWrapper : ICloudBlockBlob
    {
        private readonly CloudBlockBlob _cloudBlockBlob;

        public CloudBlockBlobWrapper(CloudBlockBlob cloudBlockBlob)
        {
            _cloudBlockBlob = cloudBlockBlob;
        }

        public Task UploadFromStreamAsync(Stream stream)
        {
            return _cloudBlockBlob.UploadFromStreamAsync(stream);
        }
    }
}