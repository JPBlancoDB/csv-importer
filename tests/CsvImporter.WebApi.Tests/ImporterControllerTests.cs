using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvImporter.WebApi.Abstractions;
using CsvImporter.WebApi.Controllers;
using FizzWare.NBuilder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace CsvImporter.WebApi.Tests
{
    public class ImporterControllerTests
    {
        private readonly Mock<IValidator> _validatorMock;
        private readonly ImporterController _importerController;
        
        public ImporterControllerTests()
        {
            _validatorMock = new Mock<IValidator>();
            _importerController = new ImporterController(_validatorMock.Object);
        }
        
        [Fact]
        public async Task Post_ShouldInvokeValidatorWithFormFiles()
        {
            // Arrange
            var context = SetupContext();

            // Act
            await _importerController.Post();

            // Assert
            _validatorMock.Verify(v => v.Validate(context.Request.Form.Files), Times.Once);
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
