using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using CsvImporter.WebApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Validators
{
    public class ContentTypeCsvValidatorTests
    {
        private readonly IValidator _validator;
        private readonly Mock<IValidationResultFactory> _validationResultFactoryMock;
        
        public ContentTypeCsvValidatorTests()
        {
            _validationResultFactoryMock = new Mock<IValidationResultFactory>();
            _validator = new ContentTypeCsvValidator(_validationResultFactoryMock.Object);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationResultError_WhenContentTypeIsNotCsv()
        {
            // Arrange
            var formFileCollection = CreateFormFileCollection("text/plain");

            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.ContentTypeInvalid), Times.Once);
        }

        [Fact]
        public void Validate_ShouldInvokeValidationSucceeded_WhenContentTypeIsCsv()
        {
            // Arrange
            var formFileCollection = CreateFormFileCollection("text/csv");

            // Act
            _validator.Validate(formFileCollection);

            // Assert
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultError(ErrorMessages.ContentTypeInvalid), Times.Never);
            _validationResultFactoryMock.Verify(v => v.CreateValidationResultSucceeded(), Times.Once);
        }

        private static FormFileCollection CreateFormFileCollection(string contentType)
        {
            var formFile = new Mock<IFormFile>();
            formFile.Setup(x => x.ContentType).Returns(contentType);

            return new FormFileCollection {formFile.Object};
        }
    }
}