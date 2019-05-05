using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IValidator
    {
        ValidationResult Validate(IFormFileCollection formFiles);
    }
}