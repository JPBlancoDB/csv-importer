using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Validators
{
    public abstract class ValidatorBase : IValidator
    {
        private IValidator _nextStep;
        protected readonly IValidationResultFactory ValidationResultFactory;

        protected ValidatorBase(IValidationResultFactory validationResultFactory)
        {
            ValidationResultFactory = validationResultFactory;
        }

        public IValidator NextStep(IValidator validator)
        {
            _nextStep = validator;

            return _nextStep;
        }

        public virtual ValidationResult Validate(IFormFileCollection formFileCollection)
        {
            return _nextStep != null 
                ? _nextStep.Validate(formFileCollection) 
                : ValidationResultFactory.CreateValidationResultSucceeded();
        }
    }
}