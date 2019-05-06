using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvImporter.Common.WebJobs.Abstractions;
using Microsoft.Azure.ServiceBus;

namespace CsvImporter.Common.WebJobs.Services
{
    public class ServiceBusService : IServiceBusService
    {
        private readonly IKeyVaultService _keyVaultService;
        private readonly ITopicClient _topicClient;
        private readonly ISubscriptionClient _subscriptionClient;
        
        public ServiceBusService(IKeyVaultService keyVaultService)
        {
            _keyVaultService = keyVaultService;
            _subscriptionClient = CreateSubscriptionClient();
            _topicClient = CreateClient();
        }
        
        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
	
            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }
        
        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }

        private ITopicClient CreateClient()
        {
            return new TopicClient(_keyVaultService.GetSecret("ServiceBusConnectionString"), _keyVaultService.GetSecret("ServiceBus:TopicName"));
        }

        private ISubscriptionClient CreateSubscriptionClient()
        {
            return new SubscriptionClient(
                _keyVaultService.GetSecret("ServiceBusConnectionString"),
                _keyVaultService.GetSecret("ServiceBus:TopicName"),
                _keyVaultService.GetSecret("ServiceBus:SubscriptionName"));
        }
    }
}