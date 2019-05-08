using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services.Azure;
using FizzWare.NBuilder;
using Microsoft.Azure.ServiceBus;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace CsvImporter.WebApi.Tests.Services
{
    public class ServiceBusQueueServiceTests
    {
        private readonly IQueueService _queueService;
        private readonly Mock<IServiceBusQueueFactory> _serviceBusQueueFactoryMock;

        public ServiceBusQueueServiceTests()
        {
            _serviceBusQueueFactoryMock = new Mock<IServiceBusQueueFactory>();
            _queueService = new ServiceBusQueueService(_serviceBusQueueFactoryMock.Object);
        }

        [Fact]
        public void Publish_ShouldInvokeFactoryToCreateServiceBusTopicClient()
        {
            // Arrange & Act
            _queueService.Publish(It.IsAny<JobDto>());

            // Assert
            _serviceBusQueueFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
        }
        
        [Fact]
        public void Publish_ShouldInvokeFactoryToCreateServiceBusMessage_WithJob()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            
            // Act
            _queueService.Publish(job);

            // Assert
            _serviceBusQueueFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
            _serviceBusQueueFactoryMock.Verify(v => v.CreateMessage(JsonConvert.SerializeObject(job)), Times.Once);
        }
        
        [Fact]
        public void Publish_ShouldInvokeTopicClientSendAsync_WithServiceBusMessage()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            var queueClientMock = new Mock<IQueueClient>();

            _serviceBusQueueFactoryMock
                .Setup(s => s.CreateQueueClient())
                .Returns(queueClientMock.Object);
            
            // Act
            _queueService.Publish(job);

            // Assert
            _serviceBusQueueFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
            _serviceBusQueueFactoryMock.Verify(v => v.CreateMessage(JsonConvert.SerializeObject(job)), Times.Once);
            queueClientMock.Verify(v => v.SendAsync(It.IsAny<Message>()), Times.Once);
        }        
    }
}