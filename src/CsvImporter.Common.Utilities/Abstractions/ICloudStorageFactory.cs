namespace CsvImporter.Common.Utilities.Abstractions
{
    public interface ICloudStorageFactory
    {
        ICloudBlobContainer CreateAzureBlobContainer();
    }
}