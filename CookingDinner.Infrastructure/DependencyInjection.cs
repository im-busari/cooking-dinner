using CookingDinner.Application.Common.Interfaces;
using CookingDinner.Application.Common.Interfaces.Persistance;
using CookingDinner.Application.Common.Interfaces.Services;
using CookingDinner.Infrastructure.Authentication;
using CookingDinner.Infrastructure.Persistence;
using CookingDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookingDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}