using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Reach.Content.Views;
using Reach.Silo.Agents.PluginModel;
using Reach.Silo.Content.ServiceModel;
using System.ComponentModel;

namespace Reach.Silo.Agents.Plugins;

public class ComponentDefinitionPlugin
{
    private IGrainFactory _grainFactory;
    private IComponentDefinitionViewReadRepository _componentDefinitionViewReadRepository;
    private ILogger<FieldDefinitionPlugin> _logger;
    public ComponentDefinitionPlugin(
        ILogger<FieldDefinitionPlugin> logger,
        IGrainFactory grainFactory,
        IComponentDefinitionViewReadRepository componentDefinitionViewReadRepository)
    {
        _logger = logger;
        _grainFactory = grainFactory;
        _componentDefinitionViewReadRepository = componentDefinitionViewReadRepository;
    }

    [KernelFunction("get_component_definitions")]
    [Description("Gets a list of all the component definitions and their fields")]
    public async Task<ComponentDefinitionView[]> GetFieldDefinitions(TenancyModel model)
    {
        var result = await _componentDefinitionViewReadRepository.Query(
            model.OrganizationId,
            model.HubId
        );

        return result.ToArray();
    }
}
