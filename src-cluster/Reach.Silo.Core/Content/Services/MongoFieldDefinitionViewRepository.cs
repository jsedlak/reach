using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoFieldDefinitionViewRepository : IFieldDefinitionViewWriteRepository, IFieldDefinitionViewReadRepository
{
    public const string CollectionName = "field_defns";

    private readonly IMongoDatabase _database;


    public MongoFieldDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
    {
        _database = mongoClient.GetDatabase(options.Value.Database);
    }

    public async Task<FieldDefinitionView?> Get(Guid id)
    {
        return await _database.GetCollection<FieldDefinitionView>(CollectionName)
            .Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<FieldDefinitionView?> Get(string key)
    {
        return await _database.GetCollection<FieldDefinitionView>(CollectionName)
            .Find(x => x.Key == key).FirstOrDefaultAsync();
    }

    public Task<IQueryable<FieldDefinitionView>> Query()
    {
        var result = _database
            .GetCollection<FieldDefinitionView>(CollectionName)
            .AsQueryable();

        return Task.FromResult((IQueryable<FieldDefinitionView>)result);
    }

    public async Task Upsert(FieldDefinitionView fieldDefinitionView)
    {
        var col = _database.GetCollection<FieldDefinitionView>(CollectionName);
        await col.ReplaceOneAsync(
            m => m.Id == fieldDefinitionView.Id,
            fieldDefinitionView,
            new ReplaceOptions() { IsUpsert = true }
        );
    }

    public async Task Delete(Guid id)
    {
        var col = _database.GetCollection<FieldDefinitionView>(CollectionName);
        await col.DeleteOneAsync(x => x.Id == id);
    }
}
