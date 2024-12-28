using Reach.Content.Commands.Editors;
using Reach.Content.Events.Editors;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Content.Grains.EditorDefinitions;

public class EditorDefinitionGrain : StreamingEventSourcedGrain<EditorDefinition, BaseEditorDefinitionEvent>, IEditorDefinitionGrain
{
    public EditorDefinitionGrain()
        : base(GrainConstants.EditorDefinition_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        await Raise(new EditorDefinitionCreatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            EditorType = command.EditorType
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetParameters(SetEditorDefinitionParametersCommand command)
    {
        await Raise(new EditorDefinitionParametersSetEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Parameters = command.Parameters
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> Delete(DeleteEditorDefinitionCommand command)
    {
        await Raise(new EditorDefinitionDeletedEvent(command.AggregateId, command.OrganizationId, command.HubId));

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }
}
