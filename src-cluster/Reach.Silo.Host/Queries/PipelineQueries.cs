using Reach.Pipelines.Views;
using Reach.Silo.Pipelines.ServiceModel;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class PipelineQueries
{
    public Task<IQueryable<PipelineView>> Pipelines(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service] IPipelineViewReadRepository repository) =>
        repository.Query(Guid.Parse(organizationId), Guid.Parse(hubId));
}