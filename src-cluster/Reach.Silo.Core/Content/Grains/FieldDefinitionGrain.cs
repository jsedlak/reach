using Reach.Content.Commands.Fields;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public class FieldDefinitionGrain : Grain, IFieldDefinitionGrain
{
    public Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        return Task.FromResult(new CommandResponse()
        {
            IsSuccess = true
        });
    }


    public Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command)
    {
        return Task.FromResult(new CommandResponse() { IsSuccess = true });
    }

    public Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command)
    {
        return Task.FromResult(new CommandResponse() { IsSuccess = true });
    }

    public Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command)
    {
        return Task.FromResult(new CommandResponse() { IsSuccess = true });
    }
}
