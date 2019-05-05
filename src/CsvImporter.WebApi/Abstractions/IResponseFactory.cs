using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IResponseFactory
    {
        IActionResult CreateResponse(ValidationResult validationResult);
    }
}