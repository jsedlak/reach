using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.Views;
using Reach.Silo.ConfigModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

internal class MongoFieldDefinitionViewRepository : MongoAggregateViewRepository<FieldDefinitionView>,
    IFieldDefinitionViewWriteRepository, IFieldDefinitionViewReadRepository
{
    private const string CollectionName = "field_defns";
    
    public MongoFieldDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }
}