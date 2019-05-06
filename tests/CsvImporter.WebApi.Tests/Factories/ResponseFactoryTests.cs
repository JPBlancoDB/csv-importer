using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Domain;
using CsvImporter.WebApi.Factories;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CsvImporter.WebApi.Tests.Factories
{
    public class ResponseFactoryTests
    {
        private readonly IResponseFactory _responseFactory;

        public ResponseFactoryTests()
        {
            _responseFactory = new ResponseFactory();
        }

        [Fact]
        public void CreateResponse_ShouldReturnBadRequest_WhenIsCalledWithValidationResult()
        {
            // Arrange
            var validationResult = Builder<ValidationResult>.CreateNew().Build();
            
            // Act
            var result = _responseFactory.CreateResponse(validationResult);
            
            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            ((BadRequestObjectResult)result).Value.Should().Be(validationResult.ErrorMessage);
        }

        [Fact]
        public void CreateResponse_ShouldReturnOkResult_WhenIsCalledWithJobDto()
        {
            // Arrange
            var jobDto = Builder<JobDto>.CreateNew().Build();
            
            // Act
            var result = _responseFactory.CreateResponse(jobDto);
            
            // Assert
            result.Should().BeOfType<AcceptedAtRouteResult>();
            ((AcceptedAtRouteResult) result).Value.Should().Be(jobDto);
        }
    }
}