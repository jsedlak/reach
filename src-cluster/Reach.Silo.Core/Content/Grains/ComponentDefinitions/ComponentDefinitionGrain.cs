using Microsoft.Extensions.Options;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Events.ComponentDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Configuration;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;
using System.ComponentModel;

namespace Reach.Silo.Content.Grains.ComponentDefinitions;

public sealed class ComponentDefinitionGrain :
    StreamingEventSourcedGrain<ComponentDefinition, BaseComponentDefinitionEvent>, IComponentDefinitionGrain
{
    private readonly ViewManagementOptions _viewOptions;

    public ComponentDefinitionGrain(IOptions<ViewManagementOptions> viewOptions)
        : base(GrainConstants.ComponentDefinition_EventStream)
    {
        _viewOptions = viewOptions.Value;
    }

    protected override async Task Raise(BaseComponentDefinitionEvent @event)
    {
        if (!_viewOptions.UseDirectCommunication)
        {
            await base.Raise(@event);
            return;
        }

        var viewGrain = GrainFactory.GetGrain<IComponentDefinitionViewGrain>(this.GetPrimaryKeyString());

        switch (@event)
        {
            case ComponentDefinitionCreatedEvent createEvent:
                await viewGrain.Handle(createEvent);
                break;
            case ComponentDefinitionDeletedEvent deleteEvent:
                await viewGrain.Handle(deleteEvent);
                break;
            case ComponentDefinitionRenamedEvent renameEvent:
                await viewGrain.Handle(renameEvent);
                break;
            case FieldAddedToComponentDefinitionEvent fieldAddedEvent:
                await viewGrain.Handle(fieldAddedEvent);
                break;
            case FieldRemovedFromComponentDefinitionEvent fieldRemovedEvent:
                await viewGrain.Handle(fieldRemovedEvent);
                break;
            case ComponentDefinitionRendererSetEvent rendererEvent:
                await viewGrain.Handle(rendererEvent);
                break;
        }
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        // allow querying of our internal state via an extension
        var ext = new ComponentDefinitionQueryExtension(() => ConfirmedState);
        GrainContext.SetComponent<IComponentDefinitionQueryExtension>(ext);

        return base.OnActivateAsync(cancellationToken);
    }

    public async Task<CommandResponse> AddField(AddFieldToComponentDefinitionCommand command)
    {
        await Raise(new FieldAddedToComponentDefinitionEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Field = new Field()
            {
                Name = command.Name,
                Slug = command.Slug,
                DefinitionId = command.FieldDefinitionId,
                Value = string.Empty
            }
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Create(CreateComponentDefinitionCommand command)
    {
        await Raise(new ComponentDefinitionCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeleteComponentDefinitionCommand command)
    {
        await Raise(new ComponentDefinitionDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> RemoveField(RemoveFieldFromComponentDefinitionCommand command)
    {
        await Raise(
            new FieldRemovedFromComponentDefinitionEvent(command.OrganizationId, command.HubId, command.AggregateId)
            {
                FieldId = command.FieldId
            });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenameComponentDefinitionCommand command)
    {
        await Raise(new ComponentDefinitionRenamedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> SetRendererDefinition(SetComponentDefinitionRendererCommand command)
    {
        await Raise(new ComponentDefinitionRendererSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
        });

        return CommandResponse.Success();
    }
}