using FluentResults;

namespace CookingDinner.Application.Common.Errors;
public class DuplicateEmailError : IError 
{
    public string Message =>  "User with given email already exists";
    public Dictionary<string, object> Metadata => throw new NotImplementedException();
    public List<IError> Reasons => throw new NotImplementedException();
}