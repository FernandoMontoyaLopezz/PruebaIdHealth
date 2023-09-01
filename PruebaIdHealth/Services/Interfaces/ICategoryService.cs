using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Services.Interfaces;

public interface ICategoryService
{

    Task<List<Category>> GetAsync();
    Task CreateAsync(Category category);
    Task UpdateAsync(string id, Category category);
    Task DeleteAsync(string id);
    Task AddProductToCategoryAsync(string id, string productId);

}