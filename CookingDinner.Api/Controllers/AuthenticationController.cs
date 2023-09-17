using CookingDinner.Application.Services.Authentication;
using CookingDinner.Application.Services.Authentication.Commands;
using CookingDinner.Application.Services.Authentication.Queries;
using CookingDinner.Contracts.Authentication;
using CookingDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CookingDiner.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ApiController
{
    private IAuthenticationCommandService _authenticationCommandService;
    private IAuthenticationQueryService _authenticationQueryService;

    public AuthenticationController(
        IAuthenticationCommandService authenticationCommandService,
        IAuthenticationQueryService authenticationQueryService)
    {
        _authenticationCommandService = authenticationCommandService;
        _authenticationQueryService = authenticationQueryService;
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register(RegisterRequest request)
    {
        ErrorOr<AuthenticationResult> registerResult =
            _authenticationCommandService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        
        return registerResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
        
        // return registerResult.Match(
        //     authResult => Ok(MapAuthResult(authResult)),
        //     error => Problem(statusCode: (int)error.StatusCode, title: error.ErrorMessage));
    }
    
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginRequest request)
    {
        ErrorOr<AuthenticationResult> loginResult =
            _authenticationQueryService.Login(request.Email, request.Password);

        if (loginResult.IsError && loginResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(
                statusCode: StatusCodes.Status401Unauthorized, 
                title: loginResult.FirstError.Description);
        }
        
        return loginResult.Match(
            authResult => Ok(MapAuthResult(authResult)),
            errors => Problem(errors));
    }
    
    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult) => new (
        authResult.User.Id,
        authResult.User.FirstName,
        authResult.User.LastName,
        authResult.User.Email,
        authResult.Token
    );
    
}