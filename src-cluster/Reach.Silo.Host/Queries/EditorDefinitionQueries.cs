using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class EditorDefinitionQueries
{
    public Task<IQueryable<EditorDefinitionView>> EditorDefinitions(
        [GlobalState]string tenantId, 
        [Service] IEditorDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(tenantId));
}


[ExtendObjectType("Query")]
public class FieldDefinitionQueries
{
    public Task<IQueryable<FieldDefinitionView>> FieldDefinitions(
        [GlobalState] string tenantId, 
        [Service] IFieldDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(tenantId));
}
