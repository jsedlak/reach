using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Silo.Agents.PluginModel;
using Reach.Silo.Content.GrainModel;
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
        var result = await _fieldDefinitionViewReadRepository.Query(
            model.OrganizationId,
            model.HubId
        );

        return result.ToArray();
    }

    [KernelFunction("create_field_definition")]
    [Description("Creates a field definition.")]
    public async Task<CommandResponse> CreateFieldDefinition(CreateFieldDefinitionCommand command)
    {
        var aggId = new AggregateId(command.OrganizationId, command.HubId, command.AggregateId);
        
        var fieldDefnGrain = _grainFactory.GetGrain<IFieldDefinitionGrain>(aggId);

        return await fieldDefnGrain.Create(command);
    }

    [KernelFunction("set_field_definition_parameters")]
    [Description("Sets the parameters on a field definition")]
    public async Task<CommandResponse> SetFieldDefinitionParameters(SetFieldDefinitionEditorParametersCommand command)
    {
        var aggId = new AggregateId(command.OrganizationId, command.HubId, command.AggregateId);

        var fieldDefnGrain = _grainFactory.GetGrain<IFieldDefinitionGrain>(aggId);

        return await fieldDefnGrain.SetEditorParameters(command);
    }
}
