using Reach.Content.Commands.ComponentDefinitions;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

/// <summary>
/// Defines the shape of a component
/// </summary>
public interface IComponentDefinitionGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateComponentDefinitionCommand command);

    Task<CommandResponse> Delete(DeleteComponentDefinitionCommand command);

    Task<CommandResponse> AddField(AddFieldToComponentDefinitionCommand command);

    Task<CommandResponse> RemoveField(RemoveFieldFromComponentDefinitionCommand command);

    Task<CommandResponse> Rename(RenameComponentDefinitionCommand command);

    Task<CommandResponse> SetRendererDefinition(SetComponentDefinitionRendererCommand command);
}