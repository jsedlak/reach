using Microsoft.SemanticKernel;
using Reach.Content.Views;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Host.Agents.PluginModel;
using System.ComponentModel;

namespace Reach.Silo.Host.Agents.Plugins;

public class FieldDefinitionPlugin
{
    private IGrainFactory _grainFactory;
    private IFieldDefinitionViewReadRepository _fieldDefinitionViewReadRepository;
    private Kernel _kernel;
    private ILogger<FieldDefinitionPlugin> _logger;
    public FieldDefinitionPlugin(
        ILogger<FieldDefinitionPlugin> logger,
        Kernel kernel,
        IGrainFactory grainFactory,
        IFieldDefinitionViewReadRepository fieldDefinitionViewReadRepository)
    {
        _logger = logger;
        _kernel = kernel;
        _grainFactory = grainFactory;
        _fieldDefinitionViewReadRepository = fieldDefinitionViewReadRepository;
    }

    [KernelFunction("get_field_definitions")]
    [Description("Gets a list of all the field definitions and their parameters")]
    public async Task<FieldDefinitionView[]> GetFieldDefinitions(TenancyModel model)
    {
        _logger.LogInformation("Incoming model: {Model}", model);

        var orgId = _kernel.Data["OrganizationId"];
        var hubId = _kernel.Data["HubId"];

        if(orgId == null || hubId == null)
        {
            return [];
        }

        var result = await _fieldDefinitionViewReadRepository.Query(
            (Guid)orgId, 
            (Guid)hubId
        );

        return result.ToArray();
    }
}
