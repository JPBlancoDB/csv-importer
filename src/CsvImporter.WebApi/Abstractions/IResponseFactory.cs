using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IResponseFactory
    {
        IActionResult CreateResponse(ValidationResult validationResult);
        IActionResult CreateResponse(JobDto job);
    }
}