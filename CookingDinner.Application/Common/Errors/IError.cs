using System.Net;

namespace CookingDinner.Application.Common.Errors;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}