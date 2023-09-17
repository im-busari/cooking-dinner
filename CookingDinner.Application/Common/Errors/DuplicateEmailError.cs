using System.Net;

namespace CookingDinner.Application.Common.Errors;

// public record struct DuplicateEmailError : IError();

public class DuplicateEmailError : IError
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage => "User with given email already exists";
}