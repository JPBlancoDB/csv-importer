using System;
using System.Linq;
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
        private readonly ICsvImporterService _csvImporterService;

        public ImporterController(IValidator validator, IResponseFactory responseFactory, ICsvImporterService csvImporterService)
        {
            _validator = validator;
            _responseFactory = responseFactory;
            _csvImporterService = csvImporterService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var formFiles = Request.Form.Files;
            
            var validationResult = _validator.Validate(formFiles);

            if (!validationResult.Success)
                return _responseFactory.CreateResponse(validationResult);

            var result = await _csvImporterService.ExecuteProcess(formFiles.Single());
            
            return _responseFactory.CreateResponse(result);
        }

        [HttpGet("{jobId:guid}", Name = "getJobStatus")]
        public ActionResult GetStatus(Guid jobId)
        {
            return Ok();
        }
    }
}