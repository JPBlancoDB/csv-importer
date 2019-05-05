using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Services
{
    public class CsvImporterService : ICsvImporterService
    {
        public Task<JobDto> ExecuteProcess(IFormFile single)
        {
            throw new System.NotImplementedException();
        }
    }
}