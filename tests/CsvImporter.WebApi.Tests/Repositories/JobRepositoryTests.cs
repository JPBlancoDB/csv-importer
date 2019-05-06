using System;
using System.Linq;
using AutoMapper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.Common.Contracts.Exceptions;
using CsvImporter.Common.Contracts.Profiles;
using CsvImporter.Common.Utilities;
using CsvImporter.WebApi.Repositories;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CsvImporter.WebApi.Tests.Repositories
{
    public class JobRepositoryTests
    {
        [Fact]
        public void Create_ShouldSaveJobEntity()
        {
            // Arrange
            var jobDto = CreateJobDto();

            using (var dbContext = new JobDbContext(CreateDbContextOptions("InMemoryDatabase_Create")))
            {
                var jobsRepository = new JobsRepository(SetupAutoMapper(), dbContext);

                // Act
                var result = jobsRepository.Create(jobDto);

                // Assert
                var jobEntity = dbContext.Jobs.Single(w => w.JobId == jobDto.JobId);
                jobEntity.FileName.Should().Be(jobDto.JobId.ToString());

                result.FileName.Should().Be(jobEntity.OriginalFileName);
                result.JobId.Should().Be(jobEntity.JobId);
                result.DateCreated.Should().Be(jobEntity.DateCreated);
                result.JobStatus.Should().Be(EnumUtility.GetValue(jobEntity.JobStatus));
                result.DateLastModified.Should().BeNull();
            }
        }

        private static JobDto CreateJobDto()
        {
            return Builder<JobDto>
                .CreateNew()
                .With(w => w.DateLastModified = null)
                .With(w => w.JobStatus = EnumUtility.GetValue(JobStatus.Created))
                .Build();
        }

        [Fact]
        public void Update_ShouldSaveJobEntityWithNewStatusAndDateLastModified()
        {
            // Arrange
            var jobDto = CreateJobDto();
            var expectedStatus = JobStatus.Queued;

            using (var dbContext = new JobDbContext(CreateDbContextOptions("InMemoryDatabase_Update")))
            {
                var jobsRepository = new JobsRepository(SetupAutoMapper(), dbContext);
                var createdJob = jobsRepository.Create(jobDto);

                // Act
                var updatedJob = jobsRepository.Update(createdJob, expectedStatus);

                // Assert
                updatedJob.DateCreated.Should().Be(createdJob.DateCreated);

                createdJob.DateLastModified.Should().BeNull();
                updatedJob.DateLastModified.Should().NotBeNull();

                createdJob.JobStatus.Should().Be(jobDto.JobStatus);
                updatedJob.JobStatus.Should().Be(EnumUtility.GetValue(expectedStatus));
            }
        }
        
        [Fact]
        public void Get_ShouldReturnJobDto()
        {
            // Arrange
            var jobDto = CreateJobDto();

            using (var dbContext = new JobDbContext(CreateDbContextOptions("InMemoryDatabase_Get")))
            {
                var jobsRepository = new JobsRepository(SetupAutoMapper(), dbContext);
                jobsRepository.Create(jobDto);

                // Act
                var job = jobsRepository.Get(jobDto.JobId);

                // Assert
                job.JobId.Should().Be(jobDto.JobId);
                job.FileName.Should().Be(jobDto.FileName);
                job.JobStatus.Should().Be(jobDto.JobStatus);
            }
        }

        [Fact]
        public void Get_ShouldThrowException_WhenJobIdNotFound()
        {
            // Arrange
            using (var dbContext = new JobDbContext(CreateDbContextOptions("InMemoryDatabase_GetNull")))
            {
                var jobsRepository = new JobsRepository(SetupAutoMapper(), dbContext);
                var jobId = Guid.Empty;
                
                // Act
                var exception = new Action(() => jobsRepository.Get(jobId));

                // Assert
                exception
                    .Should()
                    .Throw<JobNotFoundException>()
                    .WithMessage(string.Format("JobId: {0} not found.", jobId));
            }
        }
        
        private static DbContextOptions<JobDbContext> CreateDbContextOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<JobDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        private static IMapper SetupAutoMapper()
        {
            var config = new MapperConfiguration(options => { options.AddProfile(new JobProfile()); });

            return config.CreateMapper();
        }
    }
}