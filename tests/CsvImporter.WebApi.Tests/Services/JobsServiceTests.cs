using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Services;
using FizzWare.NBuilder;
using FluentAssertions;
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
            var result = _jobsService.Create(fileName, guid);

            // Assert
            _jobsRepositoryMock.Verify(v => v.Create(fileName, guid), Times.Once);
        }

        [Fact]
        public void Update_ShouldInvokeJobsRepositoryUpdateOnce()
        {
            // Arrange
            var job = Builder<JobDto>.CreateNew().Build();
            var status = JobStatus.Queued;
            
            // Act
            var result = _jobsService.UpdateStatus(job, status);

            // Assert
            _jobsRepositoryMock.Verify(v => v.Update(job, status), Times.Once);
        }
    }
}