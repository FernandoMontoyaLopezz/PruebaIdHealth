using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Repositories.Interfaces;

public interface ICategoryRepository
{

    Task<List<Category>> Get();
    Task Create(Category category);
    Task Update(string id, Category category);
    Task Delete(string id);
    Task AddProductToCategory(string id, string productId);

}