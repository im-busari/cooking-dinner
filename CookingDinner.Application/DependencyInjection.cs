using CookingDinner.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CookingDinner.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}