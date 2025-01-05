using Reach.Content.ServiceModel;
using Reach.Content.Views;

namespace Reach.Silo.Host.Queries;

[ExtendObjectType("Query")]
public class EditorDefinitionQueries
{
    public async Task<IEnumerable<EditorDefinitionView>> EditorDefinitions(
        [GlobalState] string organizationId,
        [GlobalState] string hubId,
        [Service]ILogger<EditorDefinitionQueries> logger,
        [Service] IEditorDefinitionViewReadRepository repository)
    {
        logger.LogInformation("Editor Definition Query");
        logger.LogInformation($"Organization/Hub => [{organizationId} / {hubId}]");
        
        var results = await repository.Query(
            Guid.Parse(organizationId), Guid.Parse(hubId)
        );

        logger.LogInformation($"Found {results.Count()}.");
        return results;
    }
}