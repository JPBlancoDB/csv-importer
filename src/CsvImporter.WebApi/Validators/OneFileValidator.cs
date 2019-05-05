using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Validators
{
    public class OneFileValidator : ValidatorBase
    {
        public OneFileValidator(IValidationResultFactory validationResultFactory) : base(validationResultFactory)
        {
        }
        
        public override ValidationResult Validate(IFormFileCollection formFileCollection)
        {
            return formFileCollection.Count > 1 
                ? ValidationResultFactory.CreateValidationResultError(ErrorMessages.OnlyOneFile) 
                : base.Validate(formFileCollection);
        }
    }
}