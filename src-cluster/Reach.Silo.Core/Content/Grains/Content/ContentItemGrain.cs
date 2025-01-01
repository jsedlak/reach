using Reach.Content.Commands.Components;
using Reach.Content.Commands.Content;
using Reach.Content.Events.Components;
using Reach.Content.Events.Content;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Content.Grains.Content;

public sealed class ContentItemGrain : StreamingEventSourcedGrain<ContentItem, BaseContentEvent>,
    IContentItemGrain
{
    public ContentItemGrain()
        : base(GrainConstants.Content_EventStream)
    {
    }

    public async Task<CommandResponse> Create(CreateContentCommand command)
    {
        await Raise(new ContentCreatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug,
            ComponentDefinitionId = command.ComponentDefinitionId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Delete(DeleteContentCommand command)
    {
        await Raise(new ContentDeletedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {

        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Duplicate(DuplicateContentCommand command)
    {
        await Raise(new ContentDuplicatedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            TargetHubId = command.TargetHubId,
            TargetParentId = command.TargetParentId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Move(MoveContentCommand command)
    {
        await Raise(new ContentMovedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            TargetHubId = command.TargetHubId,
            TargetParentId = command.TargetParentId
        });

        return CommandResponse.Success();
    }

    public async Task<CommandResponse> Rename(RenameContentCommand command)
    {
        await Raise(new ContentRenamedEvent(command.AggregateId, command.OrganizationId, command.HubId)
        {
            Name = command.Name,
            Slug = command.Slug
        });

        return CommandResponse.Success();
    }
}
