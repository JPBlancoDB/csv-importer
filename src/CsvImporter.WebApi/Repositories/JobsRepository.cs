using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.WebApi.Abstractions;

namespace CsvImporter.WebApi.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        public JobDto Create(string fileName, Guid guid)
        {
            throw new NotImplementedException();
        }

        public JobDto Update(JobDto job, JobStatus status)
        {
            throw new NotImplementedException();
        }
    }
}