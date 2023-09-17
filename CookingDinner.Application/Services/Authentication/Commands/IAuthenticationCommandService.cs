using ErrorOr;

namespace CookingDinner.Application.Services.Authentication.Commands;

public interface IAuthenticationCommandService
{
    // Result will return either the object we have specified or a list of errors.
    public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}