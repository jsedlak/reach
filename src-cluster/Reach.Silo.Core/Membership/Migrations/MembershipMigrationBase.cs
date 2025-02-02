using Orleans;
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
            var paramResponse = await fieldDefinition.SetEditorParameters(new SetFieldDefinitionEditorParametersCommand
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

    protected IGrainFactory GrainFactory => _grainFactory;
}