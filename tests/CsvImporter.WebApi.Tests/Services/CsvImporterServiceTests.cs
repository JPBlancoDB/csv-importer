using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Services
{
    public class CsvImporterServiceTests
    {
        private readonly Mock<IJobsService> _jobServiceMock;
        private readonly Mock<ICloudStorageService> _cloudStorageServiceMock;
        private readonly Mock<IQueueService> _queueServiceMock;
        private readonly ICsvImporterService _csvImporterService;

        public CsvImporterServiceTests()
        {
            _jobServiceMock = new Mock<IJobsService>();
            _cloudStorageServiceMock = new Mock<ICloudStorageService>();
            _queueServiceMock = new Mock<IQueueService>();
            _csvImporterService = new CsvImporterService(_jobServiceMock.Object, _cloudStorageServiceMock.Object, _queueServiceMock.Object);
        }

        [Fact]
        public void ExecuteProcess_ShouldInvokeJobServiceCreateOnce()
        {
            // Arrange
            var formFile = CreateFormFile();
            
            // Act
            _csvImporterService.ExecuteProcess(formFile);

            // Assert
            _jobServiceMock.Verify(v => v.Create(formFile.FileName, It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public void ExecuteProcess_ShouldInvokeCloudStorageUploadOnce()
        {
            // Arrange
            var formFile = CreateFormFile();
            
            // Act
            _csvImporterService.ExecuteProcess(formFile);

            // Assert
            _cloudStorageServiceMock.Verify(v => v.Upload(formFile, It.IsAny<Guid>()), Times.Once);
        }
        
        [Fact]
        public void ExecuteProcess_ShouldInvokeQueueServiceOnceWithJobDto()
        {
            // Arrange
            var formFile = CreateFormFile();
            var jobDto = SetupJobService(formFile);
            
            // Act
            _csvImporterService.ExecuteProcess(formFile);

            // Assert
            _queueServiceMock.Verify(v => v.Publish(jobDto), Times.Once);
        }
        
        [Fact]
        public void ExecuteProcess_ShouldInvokeJobServiceUpdateStatusOnce()
        {
            // Arrange
            var formFile = CreateFormFile();
            var jobDto = SetupJobService(formFile);

            // Act
            _csvImporterService.ExecuteProcess(formFile);

            // Assert
            _jobServiceMock.Verify(v => v.UpdateStatus(jobDto, JobStatus.Queued), Times.Once);
        }

        private static IFormFile CreateFormFile()
        {
            return new Mock<IFormFile>().Object;
        }
        
        private JobDto SetupJobService(IFormFile formFile)
        {
            var jobDto = Builder<JobDto>.CreateNew().Build();

            _jobServiceMock
                .Setup(s => s.Create(formFile.FileName, It.IsAny<Guid>()))
                .Returns(jobDto);
            
            return jobDto;
        }
    }
}