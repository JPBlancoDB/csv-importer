using System.Threading.Tasks;
using CsvImporter.WebApi.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CsvImporter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImporterController : ControllerBase
    {
        private readonly IValidator _validator;
        private readonly IResponseFactory _responseFactory;

        public ImporterController(IValidator validator, IResponseFactory responseFactory)
        {
            _validator = validator;
            _responseFactory = responseFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var formFiles = Request.Form.Files;
            
            var validationResult = _validator.Validate(formFiles);

            if (!validationResult.Success)
                return _responseFactory.CreateResponse(validationResult);

            return Ok();
        }
    }
}