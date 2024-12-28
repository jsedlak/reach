using Reach.Content.Commands.RendererDefinitions;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

public interface IRendererDefinitionGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateRendererDefinitionCommand command);

    Task<CommandResponse> Delete(DeleteRendererDefinitionCommand command);

    Task<CommandResponse> Rename(RenameRendererDefinitionCommand command);
}