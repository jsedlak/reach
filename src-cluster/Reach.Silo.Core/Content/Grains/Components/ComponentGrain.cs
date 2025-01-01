using Reach.Content.Commands.Components;
using Reach.Content.Events.Components;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.Components;

public sealed class ComponentGrain : StreamingEventSourcedGrain<Component, BaseComponentEvent>,
    IComponentGrain
{
    public ComponentGrain()
        : base(GrainConstants.Component_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentCommand command)
    {
        var defn = base.GrainFactory.GetGrain<IComponentDefinitionGrain>(
            new AggregateId(command.OrganizationId, command.HubId, command.DefinitionId)
        );

        var queryExt = defn.AsReference<IComponentDefinitionQueryExtension>();
        var fields = await queryExt.GetFields();

        await Raise(new ComponentCreatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
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
        await Raise(new ComponentDeletedEvent(command.AggregateId, command.OrganizationId, command.HubId)
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
        await Raise(new ComponentFieldValueSetEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
        });

        return CommandResponse.Success();
    }
}