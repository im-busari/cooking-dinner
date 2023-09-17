using FluentResults;

namespace CookingDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    // Result will return either the object we have specified or a list of errors.
    public Result<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    public AuthenticationResult Login(string email, string password);
}