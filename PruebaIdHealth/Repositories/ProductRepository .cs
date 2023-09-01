using PruebaIdHealth.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using PruebaIdHealth.Repositories.Interfaces;

namespace PruebaIdHealth.Repositories;

public class ProductRepository : IProductRepository
{

    private readonly string _collectionName = "products";
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _productCollection = database.GetCollection<Product>(_collectionName);
    }

    public async Task<List<Product>> Get()
    {
        return await _productCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task Create(Product product)
    {
        await _productCollection.InsertOneAsync(product);
        return;
    }

    public async Task Update(string id, Product product)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
        UpdateDefinition<Product> update = Builders<Product>.Update.Set("Id", id);
        if (product.Sku is not null) update.Set("Sku", product.Sku);
        if (product.Name is not null) update.Set("Name", product.Name);
        if (product.Price > 0) update.Set("Price", product.Price);
        if (product.Amount > 0) update.Set("Amount", product.Amount);
        if (product.Description is not null) update.Set("Description", product.Description);
        await _productCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task Delete(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
        await _productCollection.DeleteOneAsync(filter);
        return;
    }

    public async Task AddProductToProduct(string id, string productId)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq("Id", id);
        UpdateDefinition<Product> update = Builders<Product>.Update.AddToSet<string>("ProductIds", productId);
        await _productCollection.UpdateOneAsync(filter, update);
        return;
    }

}