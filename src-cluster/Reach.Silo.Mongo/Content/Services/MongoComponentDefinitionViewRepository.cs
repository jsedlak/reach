using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.Views;
using Reach.Silo.ConfigModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

internal class MongoComponentDefinitionViewRepository : MongoAggregateViewRepository<ComponentDefinitionView>,
    IComponentDefinitionViewWriteRepository, IComponentDefinitionViewReadRepository
{
    private const string CollectionName = "component_defns";
    
    public MongoComponentDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options) 
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }
}