using PruebaIdHealth.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using PruebaIdHealth.Repositories.Interfaces;

namespace PruebaIdHealth.Repositories;

public class StoreRepository : IStoreRepository
{

    private readonly string _collectionName = "stores";
    private readonly IMongoCollection<Store> _storeCollection;

    public StoreRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _storeCollection = database.GetCollection<Store>(_collectionName);
    }

    public async Task<List<Store>> Get()
    {
        return await _storeCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task Create(Store store)
    {
        await _storeCollection.InsertOneAsync(store);
        return;
    }

    public async Task Update(string id, Store store)
    {

        FilterDefinition<Store> filter = Builders<Store>.Filter.Eq("Id", id);
        UpdateDefinition<Store> update = Builders<Store>.Update.Set("Id", id);
        if (store.Name is not null) update.Set("Name", store.Name);
        await _storeCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task Delete(string id)
    {
        FilterDefinition<Store> filter = Builders<Store>.Filter.Eq("Id", id);
        await _storeCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task AddCategoryToStore(string id, string categoryId)
    {
        FilterDefinition<Store> filter = Builders<Store>.Filter.Eq("Id", id);
        UpdateDefinition<Store> update = Builders<Store>.Update.AddToSet<string>("CategoryIds", categoryId);
        await _storeCollection.UpdateOneAsync(filter, update);
        return;
    }

}