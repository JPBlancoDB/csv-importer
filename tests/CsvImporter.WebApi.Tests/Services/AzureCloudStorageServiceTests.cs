using System;
using System.IO;
using System.Threading.Tasks;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services.Azure;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Moq;
using Xunit;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

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

        [Fact]
        public void Upload_ShouldGetBlockBlobReference_WithGuidCsvNameFile()
        {
            // Arrange
            var formFile = CreateFormFile();
            var guid = Guid.Empty;
            var cloudBlobContainer = new Mock<ICloudBlobContainer>();
            
            _cloudStorageFactoryMock
                .Setup(s => s.CreateAzureBlobContainer())
                .Returns(cloudBlobContainer.Object);
            
            // Act
            _cloudStorageService.Upload(formFile, guid);

            // Assert
            _cloudStorageFactoryMock.Verify(v => v.CreateAzureBlobContainer(), Times.Once);
            cloudBlobContainer.Verify(v => v.GetBlockBlobReference($"{guid}.csv"), Times.Once);
        }
        
        [Fact]
        public void Upload_ShouldInvokeUploadStreamAsync()
        {
            // Arrange
            var formFile = CreateFormFile();
            var guid = Guid.Empty;
            var cloudBlobContainer = new Mock<ICloudBlobContainer>();
            var cloudBlockBlob = new Mock<ICloudBlockBlob>();
            
            _cloudStorageFactoryMock
                .Setup(s => s.CreateAzureBlobContainer())
                .Returns(cloudBlobContainer.Object);
            cloudBlobContainer
                .Setup(s => s.GetBlockBlobReference($"{guid}.csv"))
                .Returns(cloudBlockBlob.Object);
            
            // Act
            _cloudStorageService.Upload(formFile, guid);

            // Assert
            _cloudStorageFactoryMock.Verify(v => v.CreateAzureBlobContainer(), Times.Once);
            cloudBlobContainer.Verify(v => v.GetBlockBlobReference($"{guid}.csv"), Times.Once);
            cloudBlockBlob.Verify(v => v.UploadFromStreamAsync(It.IsAny<Stream>()), Times.Once);
        }
        
        [Fact]
        public void Upload_ShouldThrowStorageException_WhenStorageExceptionIsThrown()
        {
            // Arrange
            var formFile = CreateFormFile();
            var guid = Guid.Empty;
            var cloudBlobContainer = new Mock<ICloudBlobContainer>();
            var cloudBlockBlob = new Mock<ICloudBlockBlob>();
            
            _cloudStorageFactoryMock
                .Setup(s => s.CreateAzureBlobContainer())
                .Returns(cloudBlobContainer.Object);
            cloudBlobContainer
                .Setup(s => s.GetBlockBlobReference($"{guid}.csv"))
                .Returns(cloudBlockBlob.Object);
            
            cloudBlockBlob
                .Setup(s => s.UploadFromStreamAsync(It.IsAny<Stream>()))
                .Throws<StorageException>();
            
            // Act
            var action = new Func<Task>(() => _cloudStorageService.Upload(formFile, guid));

            // Assert
            action.Should().Throw<StorageException>();
            _cloudStorageFactoryMock.Verify(v => v.CreateAzureBlobContainer(), Times.Once);
            cloudBlobContainer.Verify(v => v.GetBlockBlobReference($"{guid}.csv"), Times.Once);
            cloudBlockBlob.Verify(v => v.UploadFromStreamAsync(It.IsAny<Stream>()), Times.Once);
        }
      
        private static IFormFile CreateFormFile()
        {
            return new Mock<IFormFile>().Object;
        }
    }
}