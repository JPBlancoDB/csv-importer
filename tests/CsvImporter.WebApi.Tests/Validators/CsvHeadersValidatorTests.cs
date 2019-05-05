using System;
using System.IO;
using System.Text;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using CsvImporter.WebApi.Validators;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Validators
{
    public class CsvHeadersValidatorTests
    {
        private readonly IValidator _validator;
        private readonly Mock<IValidationResultFactory> _validationResultFactoryMock;
        
        public CsvHeadersValidatorTests()
        {
            _validationResultFactoryMock = new Mock<IValidationResultFactory>();
            _validator = new CsvHeadersValidator(_validationResultFactoryMock.Object);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationResultError_WhenCsvHeadersAreIncorrect()
        {
            // Arrange
            var csv = File.Create("headers-error.csv");
            csv.Write(Encoding.UTF8.GetBytes("key,product"));
            var formFile = new FormFile(csv, 0, 11, csv.Name, csv.Name);
            var formFileCollection = new FormFileCollection { formFile };
            
            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.ErrorCsvHeader), Times.Once);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationSucceeded_WhenCsvHeadersAreCorrect()
        {
            // Arrange
            var csv = File.Create("headers-ok.csv");
            csv.Write(Encoding.UTF8.GetBytes("Key,ArtikelCode,ColorCode,Description,Price,DiscountPrice,DeliveredIn,Q1,Size,Color"));
            var formFile = new FormFile(csv, 0, 83, csv.Name, csv.Name);
            var formFileCollection = new FormFileCollection { formFile };

            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.ErrorCsvHeader), Times.Never);
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultSucceeded(), Times.Once);
        }
    }
}