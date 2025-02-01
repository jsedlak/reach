using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Events.FieldDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.FieldDefinitions;

public class FieldDefinitionGrain : StreamingEventSourcedGrain<FieldDefinition, BaseFieldDefinitionEvent>, IFieldDefinitionGrain
{
    public FieldDefinitionGrain()
        : base(GrainConstants.FieldDefinition_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug,
            EditorDefinitionId = command.EditorDefinitionId,
            EditorParameters = Array.Empty<EditorParameter>()
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command)
    {
        await Raise(new FieldDefinitionEditorSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            EditorDefinitionId = command.EditorDefinitionId,
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command)
    {
        await Raise(new FieldDefinitionEditorParametersSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            EditorParameters = command.EditorParameters
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId));

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }
}
