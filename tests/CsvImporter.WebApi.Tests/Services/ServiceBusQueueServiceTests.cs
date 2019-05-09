using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Utilities.Abstractions;
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
        private readonly Mock<IServiceBusFactory> _serviceBusFactoryMock;

        public ServiceBusQueueServiceTests()
        {
            _serviceBusFactoryMock = new Mock<IServiceBusFactory>();
            _queueService = new ServiceBusQueueService(_serviceBusFactoryMock.Object);
        }

        [Fact]
        public void Publish_ShouldInvokeFactoryToCreateServiceBusTopicClient()
        {
            // Arrange & Act
            _queueService.Publish(It.IsAny<JobDto>());

            // Assert
            _serviceBusFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
        }
        
        [Fact]
        public void Publish_ShouldInvokeFactoryToCreateServiceBusMessage_WithJob()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            
            // Act
            _queueService.Publish(job);

            // Assert
            _serviceBusFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
            _serviceBusFactoryMock.Verify(v => v.CreateMessage(JsonConvert.SerializeObject(job)), Times.Once);
        }
        
        [Fact]
        public void Publish_ShouldInvokeTopicClientSendAsync_WithServiceBusMessage()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            var queueClientMock = new Mock<IQueueClient>();

            _serviceBusFactoryMock
                .Setup(s => s.CreateQueueClient())
                .Returns(queueClientMock.Object);
            
            // Act
            _queueService.Publish(job);

            // Assert
            _serviceBusFactoryMock.Verify(v => v.CreateQueueClient(), Times.Once);
            _serviceBusFactoryMock.Verify(v => v.CreateMessage(JsonConvert.SerializeObject(job)), Times.Once);
            queueClientMock.Verify(v => v.SendAsync(It.IsAny<Message>()), Times.Once);
        }        
    }
}