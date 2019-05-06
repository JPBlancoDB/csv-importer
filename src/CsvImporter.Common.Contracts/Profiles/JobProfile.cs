using System;
using AutoMapper;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.Common.Contracts.Entities;

namespace CsvImporter.Common.Contracts.Profiles
{
    public class JobProfile : Profile
    {
        public JobProfile()
        {
            CreateMap<JobDto, JobEntity>()
                .ForMember(dst => dst.OriginalFileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dst => dst.JobStatus, opt => opt.MapFrom(src => (JobStatus)Enum.Parse(typeof(JobStatus), src.JobStatus)))
                .ForMember(dst => dst.FileName, opt => opt.MapFrom(src => src.JobId))
                .ReverseMap();
        }
    }
}