using ErrorOr;

namespace CookingDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    // Result will return either the object we have specified or a list of errors.
    public ErrorOr<AuthenticationResult> Login(string email, string password);
}