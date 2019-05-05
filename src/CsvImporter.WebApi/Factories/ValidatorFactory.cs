using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Validators;

namespace CsvImporter.WebApi.Factories
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IValidationResultFactory _validationResultFactory;

        public ValidatorFactory(IValidationResultFactory validationResultFactory)
        {
            _validationResultFactory = validationResultFactory;
        }

        public IValidator CreateValidator()
        {
            var firstValidator = new FileNotNullValidator(_validationResultFactory);
            var secondValidator = new OneFileValidator(_validationResultFactory);
            var thirdValidator  = new ContentTypeCsvValidator(_validationResultFactory);
            var fourthValidator = new CsvHeadersValidator(_validationResultFactory);

            firstValidator
                .NextStep(secondValidator)
                .NextStep(thirdValidator)
                .NextStep(fourthValidator);
            
            return firstValidator;
        }
    }
}