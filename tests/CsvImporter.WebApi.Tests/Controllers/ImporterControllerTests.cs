using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvImporter.Common.Contracts.DTOs;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Controllers;
using CsvImporter.WebApi.Domain;
using FizzWare.NBuilder;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests.Controllers
{
    public class ImporterControllerTests
    {
        private readonly Mock<IValidator> _validatorMock;
        private readonly Mock<IResponseFactory> _responseFactory;
        private readonly Mock<ICsvImporterService> _csvImporterService;
        private readonly Mock<IJobsService> _jobServiceMock;
        private readonly ImporterController _importerController;
        
        public ImporterControllerTests()
        {
            _validatorMock = new Mock<IValidator>();
            _responseFactory = new Mock<IResponseFactory>();
            _csvImporterService = new Mock<ICsvImporterService>();
            _jobServiceMock = new Mock<IJobsService>();
            _importerController = new ImporterController(_validatorMock.Object, _responseFactory.Object, _csvImporterService.Object, _jobServiceMock.Object);
        }
        
        [Fact]
        public async Task Post_ShouldInvokeValidatorWithFormFiles()
        {
            // Arrange
            var formFile = MockFormFile();
            SetupValidator(formFile, new ValidationResult());

            // Act
            await _importerController.Post();

            // Assert
            _validatorMock.Verify(v => v.Validate(formFile), Times.Once);
        }


        [Fact]
        public async Task Post_ShouldInvokeResponseFactory_WhenValidationFailed_WithValidationResult()
        {
            // Arrange
            var formFile = MockFormFile();
            
            var validationResult = new ValidationResult
            {
                ErrorMessage = "error"
            }; 

            SetupValidator(formFile, validationResult);
            
            // Act
            await _importerController.Post();

            // Assert
            _responseFactory.Verify(v => v.CreateResponse(validationResult), Times.Once);
        }
        
        [Fact]
        public async Task Post_ShouldInvokeCsvImporterService_WhenValidationSucceeded()
        {
            // Arrange
            var formFile = MockFormFile();
            SetupValidator(formFile, new ValidationResult());

            // Act
            await _importerController.Post();

            // Assert
            _csvImporterService.Verify(v => v.ExecuteProcess(formFile.Single()), Times.Once);
        }
        
        [Fact]
        public async Task Post_ShouldInvokeResponseFactory_WhenValidationSucceeded_WithJobDto()
        {
            // Arrange
            var formFile = MockFormFile();
            SetupValidator(formFile, new ValidationResult());

            // Act
            await _importerController.Post();

            // Assert
            _responseFactory.Verify(v => v.CreateResponse(It.IsAny<JobDto>()), Times.Once);
        }

        [Fact]
        public void Get_ShouldInvokeJobService_AndReturnOk()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var job = Builder<JobDto>.CreateNew().Build();

            _jobServiceMock
                .Setup(s => s.GetJob(guid))
                .Returns(job);
            
            // Act
            var result = _importerController.GetStatus(guid);

            // Assert
            _jobServiceMock.Verify(v => v.GetJob(guid), Times.Once);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Get_ShouldReturnNotFound_WhenJobIsNull()
        {
            // Arrange
            var guid = Guid.NewGuid();

            _jobServiceMock
                .Setup(s => s.GetJob(guid));
            
            // Act
            var result = _importerController.GetStatus(guid);

            // Assert
            _jobServiceMock.Verify(v => v.GetJob(guid), Times.Once);
            result.Should().BeOfType<NotFoundResult>();
        }
        
        private IFormFileCollection MockFormFile()
        {
            var context = SetupContext();
            var formFile = context.Request.Form.Files;
            return formFile;
        }

        private void SetupValidator(IFormFileCollection formFile, ValidationResult validationResult)
        {
            _validatorMock
                .Setup(s => s.Validate(formFile))
                .Returns(validationResult);
        }
        
        private DefaultHttpContext SetupContext()
        {
            var context = CreateHttpContext();
            _importerController.ControllerContext = CreateControllerContext(context);
            
            return context;
        }

        private static DefaultHttpContext CreateHttpContext()
        {
            var formFile = new Mock<IFormFile>();
            var formFileCollection = new FormFileCollection { formFile.Object };
            var formCollection = new FormCollection(It.IsAny<Dictionary<string, StringValues>>(), formFileCollection);

            return Builder<DefaultHttpContext>
                .CreateNew()
                .With(x => x.Request.Form = formCollection)
                .Build();
        }
        
        private static ControllerContext CreateControllerContext(HttpContext httpContext)
        {
            var actionContext = Builder<ActionContext>
                .CreateNew()
                .With(x => x.HttpContext = httpContext)
                .With(x => x.RouteData = new RouteData())
                .With(x => x.ActionDescriptor = new ControllerActionDescriptor())
                .Build();

            return new ControllerContext(actionContext);
        }
    }
}
