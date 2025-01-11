using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Cqrs;
using Reach.Membership.Views;
using Reach.Silo.ConfigModel;
using Reach.Silo.Membership.ServiceModel;

namespace Reach.Silo.Membership.Services;

internal class MongoAccountSettingsViewRepository :
    IAccountViewReadRepository,
    IAccountViewWriteRepository
{
    private const string AccountSettings_Collection_Name = "account_settings";

    private readonly IMongoDatabase _database;

    public MongoAccountSettingsViewRepository(IOptions<MongoViewRepositoryOptions> options, IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase(options.Value.Database);
    }

    private IMongoCollection<AccountSettingsView> GetCollection()
    {
        return _database.GetCollection<AccountSettingsView>(AccountSettings_Collection_Name);
    }

    public async Task<AccountSettingsView?> GetSettings(string accountId)
    {
        var cursor = await GetCollection().FindAsync(m => m.Id == accountId);
        return cursor.FirstOrDefault();
    }

    public async Task<CommandResponse> UpsertSettings(AccountSettingsView settings)
    {
        var col = GetCollection();
        await col.ReplaceOneAsync(
            m => m.Id == settings.Id,
            settings,
            new ReplaceOptions() { IsUpsert = true }
        );

        return CommandResponse.Success();
    }
}
