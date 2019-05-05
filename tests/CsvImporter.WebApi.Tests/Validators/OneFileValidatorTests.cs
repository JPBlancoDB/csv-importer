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
    public class OneFileValidatorTests
    {
        private readonly IValidator _validator;
        private readonly Mock<IValidationResultFactory> _validationResultFactoryMock;
        
        public OneFileValidatorTests()
        {
            _validationResultFactoryMock = new Mock<IValidationResultFactory>();
            _validator = new OneFileValidator(_validationResultFactoryMock.Object);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationResultError_WhenMoreThanOneFile()
        {
            // Arrange
            var formFile = new Mock<IFormFile>();
            var formFileCollection = new FormFileCollection { formFile.Object, formFile.Object };
            
            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.OnlyOneFile), Times.Once);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationSucceeded_WhenCountIsOne()
        {
            // Arrange
            var formFile = new Mock<IFormFile>();
            var formFileCollection = new FormFileCollection { formFile.Object };

            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.OnlyOneFile), Times.Never);
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultSucceeded(), Times.Once);
        }
    }
}