using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Abstractions
{
    public interface ICsvImporterService
    {
        Task<JobDto> ExecuteProcess(IFormFile single);
    }
}