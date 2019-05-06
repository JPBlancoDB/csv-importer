using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.Common.Utilities;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using FizzWare.NBuilder;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Services
{
    public class JobsServiceTests
    {
        private readonly IJobsService _jobsService;
        private readonly Mock<IJobsRepository> _jobsRepositoryMock;

        public JobsServiceTests()
        {
            _jobsRepositoryMock = new Mock<IJobsRepository>();
            _jobsService = new JobsService(_jobsRepositoryMock.Object);
        }

        [Fact]
        public void Create_ShouldInvokeJobsRepositoryCreateOnce()
        {
            // Arrange
            const string fileName = "file";
            var guid = Guid.Empty;
            
            // Act
            _jobsService.Create(fileName, guid);

            // Assert
            _jobsRepositoryMock.Verify(v => 
                v.Create(It.Is<JobDto>(x => 
                    x.JobStatus == EnumUtility.GetValue(JobStatus.Created) 
                    && x.JobId == guid
                    && x.FileName == fileName)),
                Times.Once);
        }

        [Fact]
        public void Update_ShouldInvokeJobsRepositoryUpdateOnce()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            
            // Act
            _jobsService.UpdateStatusQueued(job);

            // Assert
            _jobsRepositoryMock.Verify(v => v.Update(job, JobStatus.Queued), Times.Once);
        }
    }
}