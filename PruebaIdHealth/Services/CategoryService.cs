using PruebaIdHealth.Entities;
using PruebaIdHealth.Repositories.Interfaces;
using PruebaIdHealth.Services.Interfaces;

namespace PruebaIdHealth.Services;

public class CategoryService : ICategoryService
{

    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<List<Category>> GetAsync()
    {
        return await _categoryRepo.Get();
    }
    public async Task CreateAsync(Category category)
    {
        if (category is not null)
        {
            await _categoryRepo.Create(category);
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task UpdateAsync(string id, Category category)
    {
        if (category is not null)
        {
            await _categoryRepo.Update(id, category);
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task DeleteAsync(string id)
    {
        await _categoryRepo.Delete(id);
        return;
    }
    public async Task AddProductToCategoryAsync(string id, string productId)
    {
        await _categoryRepo.AddProductToCategory(id, productId);
        return;
    }

}