using System.IO;
using System.Threading.Tasks;
using CsvImporter.Common.Utilities.Abstractions;
using Microsoft.Azure.Storage.Blob;

namespace CsvImporter.Common.Utilities.Wrappers
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

        public Task DownloadToStreamAsync(Stream stream)
        {
            return _cloudBlockBlob.DownloadToStreamAsync(stream);
        }
    }
}