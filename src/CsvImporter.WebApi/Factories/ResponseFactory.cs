using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CsvImporter.WebApi.Factories
{
    public class ResponseFactory : IResponseFactory
    {
        public IActionResult CreateResponse(ValidationResult validationResult)
        {
            return new BadRequestObjectResult(validationResult.ErrorMessage);
        }

        public IActionResult CreateResponse(JobDto job)
        {
            return new OkObjectResult(job);
        }
    }
}