using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
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
            return _jobsRepository.Create(fileName, guid);
        }

        public JobDto UpdateStatus(JobDto job, JobStatus status)
        {
            return _jobsRepository.Update(job, status);
        }
    }
}