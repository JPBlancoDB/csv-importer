using CsvImporter.Common.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CsvImporter.WebApi.Filters
{
    public class ExceptionResponseFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext exceptionContext)
        {
            if (exceptionContext.Exception.GetBaseException().GetType().BaseType == typeof(BaseException))
            {
                exceptionContext.Result = CreateExceptionResult((BaseException) exceptionContext.Exception);
            }
        }

        private static IActionResult CreateExceptionResult(BaseException exception)
        {
            return new ObjectResult(exception.Message)
            {
                StatusCode = exception.StatusCode
            };
        }
    }
}