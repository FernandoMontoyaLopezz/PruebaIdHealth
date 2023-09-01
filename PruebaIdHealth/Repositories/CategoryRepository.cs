using PruebaIdHealth.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using PruebaIdHealth.Repositories.Interfaces;

namespace PruebaIdHealth.Repositories;

public class CategoryRepository : ICategoryRepository
{

    private readonly string _collectionName = "categories";
    private readonly IMongoCollection<Category> _categoryCollection;

    public CategoryRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(_collectionName);
    }

    public async Task<List<Category>> Get()
    {
        return await _categoryCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task Create(Category category)
    {
        await _categoryCollection.InsertOneAsync(category);
        return;
    }

    public async Task Update(string id, Category category)
    {

        FilterDefinition<Category> filter = Builders<Category>.Filter.Eq("Id", id);
        UpdateDefinition<Category> update = Builders<Category>.Update.Set("Id", id);
        if (category.Name is not null) update.Set("Name", category.Name);
        await _categoryCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task Delete(string id)
    {
        FilterDefinition<Category> filter = Builders<Category>.Filter.Eq("Id", id);
        await _categoryCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task AddProductToCategory(string id, string productId)
    {
        FilterDefinition<Category> filter = Builders<Category>.Filter.Eq("Id", id);
        UpdateDefinition<Category> update = Builders<Category>.Update.AddToSet<string>("ProductIds", productId);
        await _categoryCollection.UpdateOneAsync(filter, update);
        return;
    }

}