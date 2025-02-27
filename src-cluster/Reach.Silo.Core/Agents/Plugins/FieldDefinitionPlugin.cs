using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Reach.Content.Views;
using Reach.Silo.Agents.PluginModel;
using Reach.Silo.Content.ServiceModel;
using System.ComponentModel;

namespace Reach.Silo.Agents.Plugins;

public class FieldDefinitionPlugin
{
    private IGrainFactory _grainFactory;
    private IFieldDefinitionViewReadRepository _fieldDefinitionViewReadRepository;
    private ILogger<FieldDefinitionPlugin> _logger;
    public FieldDefinitionPlugin(
        ILogger<FieldDefinitionPlugin> logger,
        IGrainFactory grainFactory,
        IFieldDefinitionViewReadRepository fieldDefinitionViewReadRepository)
    {
        _logger = logger;
        _grainFactory = grainFactory;
        _fieldDefinitionViewReadRepository = fieldDefinitionViewReadRepository;
    }

    [KernelFunction("get_field_definitions")]
    [Description("Gets a list of all the field definitions and their parameters")]
    public async Task<FieldDefinitionView[]> GetFieldDefinitions(TenancyModel model)
    {
        _logger.LogInformation("Incoming model: {Model}", model);

        //var orgId = kernel.Data["OrganizationId"];
        //var hubId = kernel.Data["HubId"];

        //if(orgId == null || hubId == null)
        //{
        //    return [];
        //}

        var result = await _fieldDefinitionViewReadRepository.Query(
            model.OrganizationId,
            model.HubId
        );

        return result.ToArray();
    }
}
