using Petl;
using ReachCms.Cqrs;
using ReachCms.Domains.Content.Commands;
using ReachCms.Domains.Content.Events;

namespace ReachCms.Domains.Content.CommandHandlers;

public sealed class CreateContentCommandHandler : IRequestHandler<CreateContentCommand, CommandResult>
{
    public Task<CommandResult> ProcessAsync(RequestContext context, CreateContentCommand command, CancellationToken cancellationToken)
    {
        // TODO: Do some application validation?

        // create the event representing this new item
        var newEvent = new ContentCreatedEvent(Guid.NewGuid())
        {
            ParentId = command.ParentId,
            TemplateId = command.TemplateId,
            Name = command.Name
        };

        return Task.FromResult(
            CommandResult.Success(newEvent)
        );
    }
}
