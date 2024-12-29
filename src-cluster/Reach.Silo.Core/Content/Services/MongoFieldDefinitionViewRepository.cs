using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoFieldDefinitionViewRepository : MongoAggregateViewRepository<FieldDefinitionView>,
    IFieldDefinitionViewWriteRepository, IFieldDefinitionViewReadRepository
{
    private const string CollectionName = "field_defns";
    
    public MongoFieldDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }
}