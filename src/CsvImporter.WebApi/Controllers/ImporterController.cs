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

        public ImporterController(IValidator validator)
        {
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var formFiles = Request.Form.Files;
            
            _validator.Validate(formFiles);

            return Ok();
        }
    }
}