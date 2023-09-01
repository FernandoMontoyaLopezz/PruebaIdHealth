using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Repositories.Interfaces;

public interface IAuthRepository
{

    Task<User> Login(Credential credentials);
    Task Register(User user);
    Task<List<User>> Get();
    Task Update(string id, User user);
    Task Delete(string id);

}