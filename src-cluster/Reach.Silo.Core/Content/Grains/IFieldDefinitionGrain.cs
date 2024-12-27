using Reach.Content.Commands.Fields;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public interface IFieldDefinitionGrain : IGrainWithStringKey
{
    Task<CommandResponse> Create(CreateFieldDefinitionCommand command);

    Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command);

    Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command);

    Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command);
}