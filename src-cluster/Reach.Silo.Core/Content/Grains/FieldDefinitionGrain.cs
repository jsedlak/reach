using Reach.Content.Commands.Fields;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public class FieldDefinitionGrain : Grain, IFieldDefinitionGrain
{
    Task<CommandResponse> IFieldDefinitionGrain.Create(CreateFieldDefinitionCommand command)
    {
        return Task.FromResult(new CommandResponse()
        {
            IsSuccess = true
        });
    }
}
