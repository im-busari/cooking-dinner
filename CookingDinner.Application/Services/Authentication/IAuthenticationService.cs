using CookingDinner.Application.Common.Errors;
using OneOf;

namespace CookingDinner.Application.Services.Authentication;

public interface IAuthenticationService
{
    public OneOf<AuthenticationResult, IError> Register(string firstName, string lastName, string email, string password);
    public AuthenticationResult Login(string email, string password);
}