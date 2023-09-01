using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Services.Interfaces;

public interface IAuthService
{

    Task<string> LoginAsync(Credential credentials);
    Task RegisterAsync(User user);
    Task<List<User>> GetAsync();
    Task UpdateAsync(string id, User user);
    Task DeleteAsync(string id);
}