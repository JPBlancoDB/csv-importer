using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using Microsoft.AspNetCore.Http;

namespace CsvImporter.WebApi.Validators
{
    public class CsvHeadersValidator : ValidatorBase
    {
        public CsvHeadersValidator(IValidationResultFactory validationResultFactory) : base(validationResultFactory)
        {
        }
        
        public override ValidationResult Validate(IFormFileCollection formFileCollection)
        {
            var formFile = formFileCollection.Single();
            
            return ValidateHeaders(formFile) 
                ? base.Validate(formFileCollection) 
                : ValidationResultFactory.CreateValidationResultError(ErrorMessages.ErrorCsvHeader);
        }
        
        private bool ValidateHeaders(IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {              
                var list = stream.ReadLine()?.Split(',');
                
                return HeadersList.SequenceEqual(list);
            }            
        }
        
        private static readonly List<string> HeadersList = new List<string>
        {
            "Key",
            "ArtikelCode",
            "ColorCode",
            "Description",
            "Price",
            "DiscountPrice",
            "DeliveredIn",
            "Q1",
            "Size",
            "Color"
        };
    }
}