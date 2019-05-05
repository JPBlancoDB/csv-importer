using System.Linq;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Validators
{
    public class ContentTypeCsvValidator : ValidatorBase
    {
        public ContentTypeCsvValidator(IValidationResultFactory validationResultFactory) : base(validationResultFactory)
        {
        }
        
        public override ValidationResult Validate(IFormFileCollection formFileCollection)
        {
            return formFileCollection.Single().ContentType != "text/csv" 
                ? ValidationResultFactory.CreateValidationResultError(ErrorMessages.ContentTypeInvalid) 
                : base.Validate(formFileCollection);
        }
    }
}