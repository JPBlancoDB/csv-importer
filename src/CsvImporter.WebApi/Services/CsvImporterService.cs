using System;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;
using CsvImporter.WebApi.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Services
{
    public class CsvImporterService : ICsvImporterService
    {
        private readonly IJobsService _jobsService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IQueueService _queueService;

        public CsvImporterService(IJobsService jobsService, ICloudStorageService cloudStorageService, IQueueService queueService)
        {
            _jobsService = jobsService;
            _cloudStorageService = cloudStorageService;
            _queueService = queueService;
        }

        public async Task<JobDto> ExecuteProcess(IFormFile formFile)
        {
            var guid = Guid.NewGuid();
            
            var job = _jobsService.Create(formFile.FileName, guid);
            
            await _cloudStorageService.Upload(formFile, guid);

            await _queueService.Publish(job);

            return _jobsService.UpdateStatus(job, JobStatus.Queued);
        }
    }
}