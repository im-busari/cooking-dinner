using CookingDinner.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CookingDiner.Api.Controllers;

[ApiController]
[Route("api/error")]
public class ErrorsController : ControllerBase
{
    [HttpPost]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred."),
        };

        return Problem(statusCode: statusCode, title: message);  // Problem(title: exception.Message, statusCode: 400);
    }
    
}