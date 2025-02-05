using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Platform.ServiceModel
{
    public interface IComponentDefinitionService
    {
        Task<CommandResponse> Create(CreateComponentDefinitionCommand command);

        Task<CommandResponse> AddFieldToComponentDefinition(AddFieldToComponentDefinitionCommand command);

        Task<IEnumerable<ComponentDefinitionView>> GetComponentDefinitions(Guid organizationId, Guid hubId, string? query = null);
    }
}