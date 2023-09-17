using CookingDinner.Domain.Entities;

namespace CookingDinner.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);