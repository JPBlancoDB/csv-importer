using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Abstractions
{
    public interface ICloudStorageService
    {
        Task Upload(IFormFile formFile, Guid guid);
    }
}