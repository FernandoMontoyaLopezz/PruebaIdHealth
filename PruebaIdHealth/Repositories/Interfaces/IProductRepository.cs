using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Repositories.Interfaces;

public interface IProductRepository
{

    Task<List<Product>> Get();
    Task Create(Product product);
    Task Update(string id, Product product);
    Task Delete(string id);

}