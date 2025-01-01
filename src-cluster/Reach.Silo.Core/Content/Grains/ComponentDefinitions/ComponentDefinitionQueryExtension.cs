using Reach.Content.Model;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Content.Model;

namespace Reach.Silo.Content.Grains.ComponentDefinitions;

public sealed class ComponentDefinitionQueryExtension : IComponentDefinitionQueryExtension
{
    private readonly Func<ComponentDefinition> _getState;

    public ComponentDefinitionQueryExtension(Func<ComponentDefinition> getState)
    {
        _getState = getState;
    }

    public Task<Field[]> GetFields()
    {
        return Task.FromResult(_getState().Fields);
    }
}