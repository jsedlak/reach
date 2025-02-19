using Reach.Content.Commands.Components;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Platform.ServiceModel;

public interface IComponentService
{
    Task<CommandResponse> Create(CreateComponentCommand command);

    Task<CommandResponse> SetFieldValue(SetComponentFieldValueCommand command);

    Task<IEnumerable<ComponentView>> GetComponents(Guid organizationId, Guid hubId, string? query = null);
}