using Reach.Content.Commands.Fields;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public interface IFieldDefinitionGrain : IGrainWithGuidKey
{
    Task<CommandResponse> Create(CreateFieldDefinitionCommand command);

    Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command);

    Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command);

    Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command);
}
