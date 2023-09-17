using ErrorOr;

namespace CookingDinner.Domain.Common.Errors;

// Those can also be placed in the Application layer.
public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Validation(
            code: "Auth.InvalidCredentials", 
            description: "Invalid credentials");
    }
}