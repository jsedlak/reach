using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Events.EditorDefinitions;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.EditorDefinitions;

public class EditorDefinitionGrain : StreamingEventSourcedGrain<EditorDefinition, BaseEditorDefinitionEvent>, IEditorDefinitionGrain
{
    public EditorDefinitionGrain()
        : base(GrainConstants.EditorDefinition_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        await Raise(new EditorDefinitionCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug,
            EditorType = command.EditorType
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetParameters(SetEditorDefinitionParametersCommand command)
    {
        await Raise(new EditorDefinitionParametersSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
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
        await Raise(new EditorDefinitionDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId));

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }
}
