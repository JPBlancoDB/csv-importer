using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebJob.CsvHandler.Abstractions;
using FizzWare.NBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CsvImporter.WebJob.CsvHandler.Tests
{
    public class MessageHandlerTests
    {
        private readonly Mock<IAzureCloudStorageService> _azureCloudStorageServiceMock;
        private readonly Mock<ICsvParser> _csvParser;
        private readonly Mock<ILogger> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IServiceBusService> _serviceBusServiceMock;
        private readonly MessageHandler _messageHandler;

        public MessageHandlerTests()
        {
            _azureCloudStorageServiceMock = new Mock<IAzureCloudStorageService>();
            _csvParser = new Mock<ICsvParser>();
            _configurationMock = new Mock<IConfiguration>();
            _serviceBusServiceMock = new Mock<IServiceBusService>();
            _messageHandler = new MessageHandler(_azureCloudStorageServiceMock.Object, _csvParser.Object, _configurationMock.Object, _serviceBusServiceMock.Object);
            _loggerMock = new Mock<ILogger>();
        }

        [Fact]
        public async Task Execute_ShouldInvokeAzureCloudService()
        {
            // Arrange
            var jobDto = Builder<JobDto>.CreateNew().Build();
            
            // Act
            await _messageHandler.Execute(jobDto, _loggerMock.Object);
            
            // Assert
            _azureCloudStorageServiceMock.Verify(v => v.GetFileStream(jobDto), Times.Once);
        }
        
        [Fact]
        public async Task Execute_ShouldInvokeParseStream()
        {
            // Arrange
            var jobDto = Builder<JobDto>.CreateNew().Build();
            var stream = new Mock<MemoryStream>().Object;
            _azureCloudStorageServiceMock
                .Setup(s => s.GetFileStream(jobDto))
                .Returns(Task.FromResult(stream));
            
            // Act
            await _messageHandler.Execute(jobDto, _loggerMock.Object);
            
            // Assert
            _csvParser.Verify(v => v.ParseStream(stream, 0), Times.Once);
        }
        
        [Fact]
        public async Task Execute_ShouldInvokeServiceBus()
        {
            // Arrange
            var jobDto = Builder<JobDto>.CreateNew().Build();
            var products = Builder<List<ProductDto>>.CreateListOfSize(2).Build();
            var stream = new Mock<MemoryStream>().Object;
            _azureCloudStorageServiceMock
                .Setup(s => s.GetFileStream(jobDto))
                .Returns(Task.FromResult(stream));
            _csvParser
                .Setup(s => s.ParseStream(stream, 0))
                .Returns(products);
            
            // Act
            await _messageHandler.Execute(jobDto, _loggerMock.Object);
            
            // Assert
            foreach (var items in products)
            {
                _serviceBusServiceMock.Verify(v => v.Publish(items), Times.Exactly(2));    
            }
        }
    }
}
