using ErrorOr;

namespace CookingDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    // Result will return either the object we have specified or a list of errors.
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
    public ErrorOr<AuthenticationResult> Login(string email, string password);
}