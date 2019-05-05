using CsvImporter.WebApi.Domain;

namespace CsvImporter.WebApi.Abstractions
{
    public interface IValidationResultFactory
    {
        ValidationResult CreateValidationResultSucceeded();
        ValidationResult CreateValidationResultError(string errorMessage);
    }
}