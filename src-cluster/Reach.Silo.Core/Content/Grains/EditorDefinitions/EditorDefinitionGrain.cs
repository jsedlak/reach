using Microsoft.Extensions.Options;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Events.EditorDefinitions;
using Reach.Content.Events.FieldDefinitions;
using Reach.Cqrs;
using Reach.Silo.Configuration;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.EditorDefinitions;

public class EditorDefinitionGrain : StreamingEventSourcedGrain<EditorDefinition, BaseEditorDefinitionEvent>, IEditorDefinitionGrain
{
    private readonly ViewManagementOptions _viewOptions;

    public EditorDefinitionGrain(IOptions<ViewManagementOptions> viewOptions)
        : base(GrainConstants.EditorDefinition_EventStream)
    {
        _viewOptions = viewOptions.Value;
    }

    protected override async Task Raise(BaseEditorDefinitionEvent @event)
    {
        if (!_viewOptions.UseDirectCommunication)
        {
            await base.Raise(@event);
            return;
        }

        var viewGrain = GrainFactory.GetGrain<IEditorDefinitionViewGrain>(this.GetPrimaryKeyString());

        switch (@event)
        {
            case EditorDefinitionCreatedEvent createEvent:
                await viewGrain.Handle(createEvent);
                break;
            case EditorDefinitionParametersSetEvent paramSetEvent:
                await viewGrain.Handle(paramSetEvent);
                break;
            case EditorDefinitionDeletedEvent deleteEvent:
                await viewGrain.Handle(deleteEvent);
                break;
        }
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
