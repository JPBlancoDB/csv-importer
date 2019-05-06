using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.Common.Utilities;
using CsvImporter.WebApi.Abstractions;

namespace CsvImporter.WebApi.Services
{
    public class JobsService : IJobsService
    {
        private readonly IJobsRepository _jobsRepository;

        public JobsService(IJobsRepository jobsRepository)
        {
            _jobsRepository = jobsRepository;
        }

        public JobDto Create(string fileName, Guid guid)
        {
            var job = new JobDto
            {
                FileName = fileName,
                JobId = guid,
                JobStatus = EnumUtility.GetValue(JobStatus.Created)
            };
            
            return _jobsRepository.Create(job);
        }

        public JobDto UpdateStatusQueued(JobDto job)
        {
            return UpdateStatus(job, JobStatus.Queued);
        }

        private JobDto UpdateStatus(JobDto job, JobStatus status)
        {
            return _jobsRepository.Update(job, status);
        }
    }
}