using Petl;
using ReachCms.Cqrs;
using ReachCms.Domains.Content.Commands;
using ReachCms.Domains.Content.Events;
using ReachCms.Domains.Content.Model;

namespace ReachCms.Domains.Content.CommandHandlers;

public sealed class RenameContentCommandHandler : IRequestHandler<RenameContentCommand, CommandResult>
{
    private readonly IEventStore<ContentItem> _eventStore;

    public RenameContentCommandHandler(IEventStore<ContentItem> eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task<CommandResult> ProcessAsync(RequestContext context, RenameContentCommand command, CancellationToken cancellationToken)
    {
        var events = await _eventStore.GetEventsForAggregateAsync(command.AggregateId);

        var item = new ContentItem();
        item.Apply(events);

        if (item.Name == command.Name)
        {
            return CommandResult.Success();
        }

        return CommandResult.Success(
            new ContentRenamedEvent(item.Id)
            {
                Name = command.Name
            }
        );
    }
}