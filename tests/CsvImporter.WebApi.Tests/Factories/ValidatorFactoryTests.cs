using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Factories;
using CsvImporter.WebApi.Validators;
using FluentAssertions;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Factories
{
    public class ValidatorFactoryTests
    {
        private readonly IValidatorFactory _validatorFactory;
        
        public ValidatorFactoryTests()
        {
            var validationResultFactoryMock = new Mock<IValidationResultFactory>();
            _validatorFactory = new ValidatorFactory(validationResultFactoryMock.Object);
        }

        [Fact]
        public void CreateValidator_ShouldReturnTypeFileNotNullValidatorAsFirstValidator()
        {
            // Act
            var result = _validatorFactory.CreateValidator();

            // Assert
            result.Should().BeOfType<FileNotNullValidator>();
        }
    }
}