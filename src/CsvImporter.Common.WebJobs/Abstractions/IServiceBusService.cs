namespace CsvImporter.Common.WebJobs.Abstractions
{
    public interface IServiceBusService
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
    }
}