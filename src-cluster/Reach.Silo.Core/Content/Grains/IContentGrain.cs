using Reach.Content.Commands.Content;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public interface IContentGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateContentCommand command);

    Task<CommandResponse> Delete(DeleteContentCommand command);

    Task<CommandResponse> Rename(RenameContentCommand comnand);

    Task<CommandResponse> Move(MoveContentCommand command);

    Task<CommandResponse> Duplicate(DuplicateContentCommand command);
}