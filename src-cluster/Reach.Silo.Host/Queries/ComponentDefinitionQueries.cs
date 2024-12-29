using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class ComponentDefinitionQueries
{
    public Task<IQueryable<ComponentDefinitionView>> ComponentDefinitions(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IComponentDefinitionViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}