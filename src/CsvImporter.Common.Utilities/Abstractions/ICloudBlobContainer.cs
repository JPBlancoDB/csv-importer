namespace CsvImporter.Common.Utilities.Abstractions
{
    public interface ICloudBlobContainer
    {
        ICloudBlockBlob GetBlockBlobReference(string containerName);
    }
}