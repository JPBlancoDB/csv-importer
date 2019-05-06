using System;
using System.Linq;
using AutoMapper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.Common.Contracts.Exceptions;
using CsvImporter.WebApi.Abstractions;

namespace CsvImporter.WebApi.Repositories
{
    public class JobsRepository : IJobsRepository
    {
        private readonly IMapper _mapper;
        private readonly JobDbContext _dbContext;

        public JobsRepository(IMapper mapper, JobDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public JobDto Create(JobDto job)
        {
            var jobEntity = _mapper.Map<JobDto, JobEntity>(job);

            _dbContext.Jobs.Add(jobEntity);
            _dbContext.SaveChanges();

            return _mapper.Map<JobEntity, JobDto>(jobEntity);
        }
        
        public JobDto Update(JobDto job, JobStatus status)
        {
            var jobEntity = GetJobEntity(job.JobId);
            
            jobEntity.JobStatus = status;
            jobEntity.DateLastModified = DateTime.Now;

            _dbContext.SaveChanges();
            
            return _mapper.Map<JobEntity, JobDto>(jobEntity);            
        }

        public JobDto Get(Guid jobId)
        {
            var jobEntity = GetJobEntity(jobId);
            
            return _mapper.Map<JobEntity, JobDto>(jobEntity);
        }
        
        private JobEntity GetJobEntity(Guid jobId)
        {
            var jobEntity = _dbContext.Jobs.SingleOrDefault(where => where.JobId == jobId);
            
            if(jobEntity == null)
                throw new JobNotFoundException(jobId);
            
            return jobEntity;
        }
    }
}