using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Platform.ServiceModel;

public interface IEditorDefinitionService
{
    Task<CommandResponse> Create(CreateEditorDefinitionCommand command);
    Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(Guid organizationId, Guid hubId, string? query = null);
}