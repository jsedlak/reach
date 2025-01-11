using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Events.ComponentDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.ComponentDefinitions;

public sealed class ComponentDefinitionGrain :
    StreamingEventSourcedGrain<ComponentDefinition, BaseComponentDefinitionEvent>, IComponentDefinitionGrain
{
    public ComponentDefinitionGrain()
        : base(GrainConstants.ComponentDefinition_EventStream)
    {
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
        await Raise(new FieldAddedToComponentDefinition(command.OrganizationId, command.HubId, command.AggregateId)
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