using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IJobsRepository
    {
        JobDto Create(JobDto job);
        JobDto Update(JobDto job, JobStatus status);
    }
}