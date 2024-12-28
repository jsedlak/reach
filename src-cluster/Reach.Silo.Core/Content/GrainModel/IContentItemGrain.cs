using Reach.Content.Commands.Content;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

public interface IContentItemGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateContentCommand command);

    Task<CommandResponse> Delete(DeleteContentCommand command);

    Task<CommandResponse> Rename(RenameContentCommand command);

    Task<CommandResponse> Move(MoveContentCommand command);

    Task<CommandResponse> Duplicate(DuplicateContentCommand command);
}