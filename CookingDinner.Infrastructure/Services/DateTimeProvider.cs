using CookingDinner.Application.Common.Interfaces.Services;

namespace CookingDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow; 
}