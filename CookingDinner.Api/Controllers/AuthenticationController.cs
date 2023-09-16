using CookingDinner.Application.Services.Authentication;
using CookingDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CookingDiner.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult =
            _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
            
        return Ok(response);
    }
    
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult =
            _authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
            
        return Ok(response);
    }
    
}