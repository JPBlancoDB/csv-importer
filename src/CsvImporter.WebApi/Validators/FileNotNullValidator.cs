using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Validators
{
    public class FileNotNullValidator : ValidatorBase
    {
        public FileNotNullValidator(IValidationResultFactory validationResultFactory) : base(validationResultFactory)
        {
        }
        
        public override ValidationResult Validate(IFormFileCollection formFileCollection)
        {
            return formFileCollection.Count == 0 
                ? ValidationResultFactory.CreateValidationResultError(ErrorMessages.CsvFileIsMandatory) 
                : base.Validate(formFileCollection);
        }
    }
}