using System;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IJobsService
    {
        JobDto Create(string fileName, Guid guid);
        JobDto UpdateStatusQueued(JobDto job);
        JobDto GetJob(Guid jobId);
    }
}