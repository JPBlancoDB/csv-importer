using System;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Services
{
    public class AzureCloudStorageServiceTests
    {
        private readonly ICloudStorageService _cloudStorageService;
        private readonly Mock<ICloudStorageFactory> _cloudStorageFactoryMock;

        public AzureCloudStorageServiceTests()
        {
            var loggerMock = new Mock<ILogger<AzureCloudStorageService>>();
            _cloudStorageFactoryMock = new Mock<ICloudStorageFactory>();
            
            _cloudStorageService = new AzureCloudStorageService(loggerMock.Object, _cloudStorageFactoryMock.Object);    
        }

        [Fact]
        public void Upload_ShouldInvokeCloudStorageFactoryCreateAzureBlobContainerOnce()
        {
            // Arrange
            var formFile = CreateFormFile();
            var guid = Guid.Empty;
            
            // Act
            _cloudStorageService.Upload(formFile, guid);

            // Assert
            _cloudStorageFactoryMock.Verify(v => v.CreateAzureBlobContainer(), Times.Once);
        }
      
        private static IFormFile CreateFormFile()
        {
            return new Mock<IFormFile>().Object;
        }
    }
}