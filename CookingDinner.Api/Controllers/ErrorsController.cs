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
        
        return Problem(title: exception.Message, statusCode: 400);
    }
    
}