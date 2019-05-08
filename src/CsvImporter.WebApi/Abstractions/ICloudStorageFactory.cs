using CsvImporter.Common.Utilities.Abstractions;

namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudStorageFactory
    {
        ICloudBlobContainer CreateAzureBlobContainer();
    }
}