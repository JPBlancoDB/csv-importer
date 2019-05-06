namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudBlobContainer
    {
        ICloudBlockBlob GetBlockBlobReference(string containerName);
    }
}