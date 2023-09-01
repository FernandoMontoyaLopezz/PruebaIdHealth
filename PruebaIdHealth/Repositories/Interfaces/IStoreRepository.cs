using PruebaIdHealth.Entities;

namespace PruebaIdHealth.Repositories.Interfaces;

public interface IStoreRepository
{

    Task<List<Store>> Get();
    Task Create(Store store);
    Task Update(string id, Store store);
    Task Delete(string id);
    Task AddCategoryToStore(string id, string categoryId);

}