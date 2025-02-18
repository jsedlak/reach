using Orleans;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Commands.Components;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Model;
using Reach.Cqrs;
using Reach.Silo.Content.GrainModel;
using Reach.Silo.Migrations;

namespace Reach.Silo.Membership.Migrations;

public abstract class MembershipMigrationBase : IMigration
{
    private readonly IGrainFactory _grainFactory;

    protected MembershipMigrationBase(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public abstract Task Forwards(Guid organizationId, Guid hubId);

    protected async Task<CommandResponse> CreateFieldDefinition(
        Guid organizationId,
        Guid hubId,
        Guid definitionId,
        string name,
        string slug,
        Guid editorDefinitionId,
        IEnumerable<EditorParameter>? parameters = null
    )
    {
        var aggId = new AggregateId(organizationId, hubId, definitionId);

        var fieldDefinition = _grainFactory.GetGrain<IFieldDefinitionGrain>(aggId);

        var createResponse = await fieldDefinition.Create(new CreateFieldDefinitionCommand(
            aggId.OrganizationId, aggId.HubId, aggId.ResourceId)
        {
            Name = name,
            Slug = slug,
            EditorDefinitionId = editorDefinitionId
        });

        if (createResponse is not { IsSuccess: true })
        {
            return createResponse;
        }

        if (parameters is not null && parameters.Any())
        {
            var paramResponse = await fieldDefinition.SetEditorParameters(new SetFieldDefinitionEditorParametersCommand(
                aggId.OrganizationId, aggId.HubId, aggId.ResourceId)
            {
                EditorParameters = parameters.ToArray()
            });

            return paramResponse;
        }

        return createResponse;
    }
    protected async Task<CommandResponse> CreateEditorDefinition(
        Guid organizationId,
        Guid hubId,
        Guid definitionId,
        string name,
        string slug,
        string type,
        IEnumerable<EditorParameterDefinition>? parameters = null
    )
    {
        var aggId = new AggregateId(organizationId, hubId, definitionId);

        var editorDefinition = _grainFactory.GetGrain<IEditorDefinitionGrain>(aggId);

        var createResponse = await editorDefinition.Create(new CreateEditorDefinitionCommand(
            aggId.OrganizationId, aggId.HubId, aggId.ResourceId)
        {
            Name = name,
            Slug = slug,
            EditorType = type
        });

        if (createResponse is not { IsSuccess: true })
        {
            return createResponse;
        }

        if (parameters is not null && parameters.Any())
        {
            var paramResponse = await editorDefinition.SetParameters(new SetEditorDefinitionParametersCommand(
                aggId.OrganizationId, aggId.HubId, aggId.ResourceId)
            {
                Parameters = parameters.ToArray()
            });

            return paramResponse;
        }

        return createResponse;
    }

    protected async Task<CommandResponse> CreateComponentDefinition(
        Guid organizationId,
        Guid hubId,
        Guid definitionId,
        string name,
        string slug
    )
    {
        var aggId = new AggregateId(organizationId, hubId, definitionId);

        var componentDefn = _grainFactory.GetGrain<IComponentDefinitionGrain>(aggId);

        var createResponse = await componentDefn.Create(new CreateComponentDefinitionCommand(aggId)
        {
            Name = name,
            Slug = slug,
            ParentId = Guid.Empty
        });

        return createResponse;
    }

    protected async Task<CommandResponse> AddFieldToComponentDefinition(
        Guid organizationId,
        Guid hubId,
        Guid definitionId,
        string name,
        string slug,
        Guid fieldDefinitionId)
    {
        var aggId = new AggregateId(organizationId, hubId, definitionId);

        var componentDefn = _grainFactory.GetGrain<IComponentDefinitionGrain>(aggId);

        var response = await componentDefn.AddField(new AddFieldToComponentDefinitionCommand(aggId)
        {
            Name = name,
            Slug = slug,
            FieldDefinitionId = fieldDefinitionId
        });

        return response;
    }

    protected async Task<CommandResponse> CreateComponent(
        Guid organizationId,
        Guid hubId,
        Guid componentId,
        string name,
        string slug,
        Guid componentDefinitionId)
    {
        var aggId = new AggregateId(organizationId, hubId, componentId);

        var component = _grainFactory.GetGrain<IComponentGrain>(aggId);

        var createResponse = await component.Create(new CreateComponentCommand(aggId)
        {
            Name = name,
            Slug = slug,
            DefinitionId = componentDefinitionId
        });

        return createResponse;
    }

    protected IGrainFactory GrainFactory => _grainFactory;
}