using CooldownsServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CooldownsServer.Services;

public class ChampionServices{

    private readonly IMongoCollection<Champion> _championCollection;
    public ChampionServices(
        IOptions<ChampionServerSettings> championDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            championDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            championDatabaseSettings.Value.DatabaseName);

        _championCollection = mongoDatabase.GetCollection<Champion>(
            championDatabaseSettings.Value.CollectionName);
    }
    public async Task<List<Champion>> GetAsync() =>
        await _championCollection.Find(_ => true).ToListAsync();

    public async Task<Champion> GetAsync(string id) =>
        await _championCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Champion newChampion) =>
        await _championCollection.InsertOneAsync(newChampion);

    public async Task UpdateAsync(string id, Champion updatedChampion) =>
        await _championCollection.ReplaceOneAsync(x => x.Id == id, updatedChampion);

    public async Task RemoveAsync(string id) =>
        await _championCollection.DeleteOneAsync(x => x.Id == id);
    public async Task<Champion> ReturnChampionByName(string name) =>
        await _championCollection.Find(x => x.ChampionName == name).FirstOrDefaultAsync();
}
