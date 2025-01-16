using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Platform.ServiceModel;

public interface IFieldDefinitionService
{
    Task<CommandResponse> Create(CreateFieldDefinitionCommand command);

    Task<IEnumerable<FieldDefinitionView>> GetFieldDefinitions(Guid organizationId, Guid hubId, string? query = null);
}