using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Services.Interfaces;

public interface IStoreService
{

    Task<List<Store>> GetAsync();
    Task CreateAsync(Store store);
    Task UpdateAsync(string id, Store store);
    Task DeleteAsync(string id);
    Task AddCategoryToStoreAsync(string id, string categoryId);

}