using PruebaIdHealth.Entities;
using PruebaIdHealth.Repositories.Interfaces;
using PruebaIdHealth.Services.Interfaces;

namespace PruebaIdHealth.Services;

public class ProductService : IProductService
{

    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<List<Product>> GetAsync()
    {
        return await _productRepo.Get();
    }
    public async Task CreateAsync(Product product)
    {
        if (product is not null)
        {
            await _productRepo.Create(product);

        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task UpdateAsync(string id, Product product)
    {
        if (product is not null)
        {
            await _productRepo.Update(id, product);

        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task DeleteAsync(string id)
    {
        await _productRepo.Delete(id);
        return;
    }

}