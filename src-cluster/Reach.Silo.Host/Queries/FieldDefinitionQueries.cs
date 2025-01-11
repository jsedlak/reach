using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class FieldDefinitionQueries
{
    public Task<IQueryable<FieldDefinitionView>> FieldDefinitions(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IFieldDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}