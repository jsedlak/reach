using Reach.Content.Commands.RendererDefinitions;
using Reach.Content.Events.RendererDefinitions;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.RendererDefinitions;

public class RendererDefinitionGrain : StreamingEventSourcedGrain<RendererDefinition, BaseRendererDefinitionEvent>,
    IRendererDefinitionGrain
{
    public RendererDefinitionGrain()
        : base(GrainConstants.RendererDefinition_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionCreatedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeleteRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionDeletedEvent(command.OrganizationId, command.HubId, command.AggregateId));
        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenameRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionRenamedEvent(command.OrganizationId, command.HubId, command.AggregateId)
        {
            Name = command.Name,
            Slug = command.Slug
        });
        
        return CommandResponse.Success();
    }
}