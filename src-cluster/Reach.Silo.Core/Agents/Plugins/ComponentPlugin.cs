using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Reach.Content.Views;
using Reach.Silo.Agents.PluginModel;
using Reach.Silo.Content.ServiceModel;
using System.ComponentModel;

namespace Reach.Silo.Agents.Plugins;

public class ComponentPlugin
{
    private IGrainFactory _grainFactory;
    private IComponentViewReadRepository _componentViewReadRepository;
    private ILogger<FieldDefinitionPlugin> _logger;
    public ComponentPlugin(
        ILogger<FieldDefinitionPlugin> logger,
        IGrainFactory grainFactory,
        IComponentViewReadRepository componentViewReadRepository)
    {
        _logger = logger;
        _grainFactory = grainFactory;
        _componentViewReadRepository = componentViewReadRepository;
    }

    [KernelFunction("get_components")]
    [Description("Gets a list of all the components (content) and their fields")]
    public async Task<ComponentView[]> GetFieldDefinitions(TenancyModel model)
    {
        var result = await _componentViewReadRepository.Query(
            model.OrganizationId,
            model.HubId
        );

        return result.ToArray();
    }
}
