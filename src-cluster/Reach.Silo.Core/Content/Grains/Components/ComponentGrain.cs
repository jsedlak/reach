using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reach.Content.Commands.Components;
using Reach.Content.Events.Components;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Configuration;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.Components;

public sealed class ComponentGrain : StreamingEventSourcedGrain<Component, BaseComponentEvent>,
    IComponentGrain
{
    private readonly ViewManagementOptions _viewOptions;
    private readonly ILogger<IComponentGrain> _logger;

    public ComponentGrain(
        IOptions<ViewManagementOptions> viewOptions, 
        ILogger<IComponentGrain> logger)
        : base(GrainConstants.Component_EventStream)
    {
        _viewOptions = viewOptions.Value;
        _logger = logger;
    }

    protected override async Task Raise(BaseComponentEvent @event)
    {
        if (!_viewOptions.UseDirectCommunication)
        {
            await base.Raise(@event);
            return;
        }

        var viewGrain = GrainFactory.GetGrain<IComponentViewGrain>(this.GetPrimaryKeyString());

        switch (@event)
        {
            case ComponentCreatedEvent createEvent:
                await viewGrain.Handle(createEvent);
                break;
            case ComponentDeletedEvent deleteEvent:
                await viewGrain.Handle(deleteEvent);
                break;
            case ComponentRenamedEvent renameEvent:
                await viewGrain.Handle(renameEvent);
                break;
            case ComponentFieldValueSetEvent fieldValueSetEvent:
                await viewGrain.Handle(fieldValueSetEvent);
                break;
        }
    }

    public async Task<CommandResponse> Create(CreateComponentCommand command)
    {
        var defn = base.GrainFactory.GetGrain<IComponentDefinitionGrain>(
            new AggregateId(command.OrganizationId, command.HubId, command.DefinitionId)
        );

        var queryExt = defn.AsReference<IComponentDefinitionQueryExtension>();
        var fields = await queryExt.GetFields();

        _logger.LogInformation($"Fields Found: {fields.Count()}");

        await Raise(new ComponentCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug,
            DefinitionId = command.DefinitionId,
            Fields = fields.Select(m => new Field(m)).ToArray()
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeleteComponentCommand command)
    {
        await Raise(new ComponentDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenameComponentCommand command)
    {
        await Raise(new ComponentRenamedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> SetFieldValue(SetComponentFieldValueCommand command)
    {
        var fieldId = command.FieldId ??
                      TentativeState.Fields.First(m => m.Slug.Equals(command.FieldKey)).Id;
        
        await Raise(new ComponentFieldValueSetEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            FieldId = fieldId,
            Value = command.Value
        });

        return CommandResponse.Success();
    }
}