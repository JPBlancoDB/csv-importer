using Microsoft.Azure.Storage.Blob;

namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudStorageFactory
    {
        CloudBlobContainer CreateAzureBlobContainer();
    }
}