using Reach.Content.Commands.RendererDefinitions;
using Reach.Content.Events.RendererDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Content.Grains.RendererDefinitions;

public class RendererDefinitionGrain : StreamingEventSourcedGrain<RendererDefinition, BaseRendererDefinitionEvent>,
    IRendererDefinitionGrain
{
    public RendererDefinitionGrain()
        : base(GrainConstants.Content_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionCreatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeleteRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionDeletedEvent(command.AggregateId, command.OrganizationId, command.HubId));
        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenameRendererDefinitionCommand command)
    {
        await Raise(new RendererDefinitionRenamedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug
        });
        
        return CommandResponse.Success();
    }
}