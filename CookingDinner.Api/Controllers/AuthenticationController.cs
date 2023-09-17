using CookingDinner.Application.Common.Errors;
using CookingDinner.Application.Services.Authentication;
using CookingDinner.Contracts.Authentication;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using OneOf;

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
        Result<AuthenticationResult> registerResult =
            _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        if (registerResult.IsSuccess) return Ok(MapAuthResult(registerResult.Value));

        var firstError = registerResult.Errors[0];

        if (firstError is DuplicateEmailError) return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already taken.");

        return Problem();
        
        // return registerResult.Match(
        //     authResult => Ok(MapAuthResult(authResult)),
        //     error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
    }
    
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult =
            _authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
            
        return Ok(response);
    }
    
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult) => new (
        authResult.User.Id,
        authResult.User.FirstName,
        authResult.User.LastName,
        authResult.User.Email,
        authResult.Token
    );
    
}