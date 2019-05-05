using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IValidator
    {
        IValidator NextStep(IValidator validatorStep);
        ValidationResult Validate(IFormFileCollection formFileCollection);
    }
}