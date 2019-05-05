using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;

namespace CsvImporter.WebApi.Factories
{
    public class ValidationResultFactory : IValidationResultFactory
    {
        public ValidationResult CreateValidationResultSucceeded()
        {
            return new ValidationResult();
        }

        public ValidationResult CreateValidationResultError(string errorMessage)
        {
            return new ValidationResult
            {
                ErrorMessage = errorMessage
            };
        }
    }
}