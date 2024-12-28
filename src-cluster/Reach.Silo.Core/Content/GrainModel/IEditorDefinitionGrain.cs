using Petl.EventSourcing;
using Reach.Content.Commands.Editors;
using Reach.Cqrs;

namespace Reach.Silo.Content.GrainModel;

public interface IEditorDefinitionGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateEditorDefinitionCommand command);

    Task<CommandResponse> SetParameters(SetEditorDefinitionParametersCommand command);

    Task<CommandResponse> Delete(DeleteEditorDefinitionCommand command);
}
