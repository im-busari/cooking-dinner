using CookingDinner.Application.Common.Errors;
using CookingDinner.Application.Common.Interfaces;
using CookingDinner.Application.Common.Interfaces.Persistance;
using CookingDinner.Domain.Common.Errors;
using CookingDinner.Domain.Entities;
using ErrorOr;

namespace CookingDinner.Application.Services.Authentication.Queries;

public class AuthenticationQueryService : IAuthenticationQueryService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public ErrorOr<AuthenticationResult> Login(string email, string password)
    {
        // 1. Validate the user exists
        if (_userRepository.GetUserByEmail(email) is not User user)
        {
            return Errors.Authentication.InvalidCredentials;
        }
        
        // 2. Validate password is correct
        if (user.Password != password) return new[] { Errors.Authentication.InvalidCredentials };
        
        // 3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResult(
            user,
            token
        );
    }

}