using PruebaIdHealth.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using PruebaIdHealth.Repositories.Interfaces;

namespace PruebaIdHealth.Repositories;

public class AuthRepository : IAuthRepository
{

    private readonly string _collectionName = "users";
    private readonly IMongoCollection<User> _userCollection;

    public AuthRepository(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(_collectionName);
    }



    public async Task<User> Login(Credential credentials)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.And(
             Builders<User>.Filter.Eq("Username", credentials.Username),
             Builders<User>.Filter.Eq("Password", credentials.Password)
            );
        return await _userCollection.Find(filter).Limit(1).SingleAsync();
    }
    public async Task Register(User user)
    {
        await _userCollection.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> Get()
    {
        return await _userCollection.Find(new BsonDocument()).ToListAsync();
    }
    public async Task Update(string id, User user)
    {

        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.Set("Id", id);
        if (user.Username is not null) update.Set("Username", user.Username);
        if (user.Password is not null) update.Set("Password", user.Password);
        if (user.Email is not null) update.Set("Email", user.Email);
        if (user.StoreId is not null) update.Set("StoreId", user.StoreId);
        await _userCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task Delete(string id)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        await _userCollection.DeleteOneAsync(filter);
        return;
    }
}