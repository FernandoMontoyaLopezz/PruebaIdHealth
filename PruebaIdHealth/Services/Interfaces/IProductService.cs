using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Services.Interfaces;

public interface IProductService
{

    Task<List<Product>> GetAsync();
    Task CreateAsync(Product product);
    Task UpdateAsync(string id, Product product);
    Task DeleteAsync(string id);

}