using Reach.Content.Commands.Components;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

public interface IComponentGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateComponentCommand command);

    Task<CommandResponse> Delete(DeleteComponentCommand command);

    Task<CommandResponse> SetFieldValue(SetComponentFieldValueCommand command);

    Task<CommandResponse> Rename(RenameComponentCommand command);
}