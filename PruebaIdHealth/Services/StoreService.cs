using PruebaIdHealth.Entities;
using PruebaIdHealth.Repositories.Interfaces;
using PruebaIdHealth.Services.Interfaces;

namespace PruebaIdHealth.Services;

public class StoreService : IStoreService
{

    private readonly IStoreRepository _storeRepo;

    public StoreService(IStoreRepository storeRepo)
    {
        _storeRepo = storeRepo;
    }

    public async Task<List<Store>> GetAsync()
    {
        return await _storeRepo.Get();
    }
    public async Task CreateAsync(Store store)
    {
        if (store is not null)
        {
            await _storeRepo.Create(store);
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task UpdateAsync(string id, Store store)
    {
        if (store is not null)
        {
            await _storeRepo.Update(id, store);
        }
        else
        {
            throw new InvalidDataException("Invalid request data");
        }
        return;
    }
    public async Task DeleteAsync(string id)
    {
        await _storeRepo.Delete(id);
        return;
    }
    public async Task AddCategoryToStoreAsync(string id, string categoryId)
    {
        await _storeRepo.AddCategoryToStore(id, categoryId);
        return;
    }

}