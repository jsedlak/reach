using Reach.Content.Commands.Components;
using Reach.Content.Events.Components;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

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
        await Raise(new ComponentCreatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug
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
