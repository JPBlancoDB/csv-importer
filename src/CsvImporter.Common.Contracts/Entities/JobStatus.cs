namespace CsvImporter.Common.Contracts.Entities
{
    public enum JobStatus : byte
    {
        Created = 0,
        Queued = 1,
        InProgress = 2,
        Done = 3,
        Failed = 4
    }
}