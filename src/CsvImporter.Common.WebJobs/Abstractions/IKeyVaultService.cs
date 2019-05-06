namespace CsvImporter.Common.WebJobs.Abstractions
{
    public interface IKeyVaultService
    {
        string GetSecret(string key);
    }
}