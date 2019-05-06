namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudStorageFactory
    {
        ICloudBlobContainer CreateAzureBlobContainer();
    }
}