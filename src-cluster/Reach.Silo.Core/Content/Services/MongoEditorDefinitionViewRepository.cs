using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reach.Content.ServiceModel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Services;

public class MongoEditorDefinitionViewRepository : MongoAggregateViewRepository<EditorDefinitionView>, 
    IEditorDefinitionViewReadRepository, IEditorDefinitionViewWriteRepository
{
    private const string CollectionName = "editor_defns";

    public MongoEditorDefinitionViewRepository(IMongoClient mongoClient, IOptions<MongoViewRepositoryOptions> options)
        : base(mongoClient, options.Value.Database, CollectionName)
    {
    }
}
