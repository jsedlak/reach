using Microsoft.Extensions.Options;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Events.FieldDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Configuration;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.FieldDefinitions;

public class FieldDefinitionGrain : StreamingEventSourcedGrain<FieldDefinition, BaseFieldDefinitionEvent>, IFieldDefinitionGrain
{
    private readonly ViewManagementOptions _viewOptions;

    public FieldDefinitionGrain(IOptions<ViewManagementOptions> viewOptions)
        : base(GrainConstants.FieldDefinition_EventStream)
    {
        _viewOptions = viewOptions.Value;
    }

    protected override async Task Raise(BaseFieldDefinitionEvent @event)
    {
        if (!_viewOptions.UseDirectCommunication)
        {
            await base.Raise(@event);
            return;
        }

        var viewGrain = GrainFactory.GetGrain<IFieldDefinitionViewGrain>(this.GetPrimaryKeyString());

        switch (@event)
        {
            case FieldDefinitionCreatedEvent createEvent:
                await viewGrain.Handle(createEvent);
                break;
            case FieldDefinitionEditorParametersSetEvent paramSetEvent:
                await viewGrain.Handle(paramSetEvent);
                break;
            case FieldDefinitionDeletedEvent deleteEvent:
                await viewGrain.Handle(deleteEvent);
                break;
            case FieldDefinitionEditorSetEvent editorSetEvent:
                await viewGrain.Handle(editorSetEvent);
                break;
        }
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
