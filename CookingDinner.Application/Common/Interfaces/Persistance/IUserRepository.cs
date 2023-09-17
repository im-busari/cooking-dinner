using CookingDinner.Domain.Entities;

namespace CookingDinner.Application.Common.Interfaces.Persistance;

public interface IUserRepository
{
    // fetching the user
    User? GetUserByEmail(string email);
    
    // adding the user
    void Add(User user);
}