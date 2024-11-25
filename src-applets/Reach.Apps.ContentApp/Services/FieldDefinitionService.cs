
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Components.Context;
using Reach.Content.Commands.Fields;
using Reach.Content.Views;
using Reach.Cqrs;

namespace Reach.Apps.ContentApp.Services;

public class FieldDefinitionService : BaseService
{
    private const string DefaultQuery = @"
    query {
        fieldDefinitions {
            id
            name
            key
            editorDefinitionId
            editorParameters {
                key
                value
            }
            editorDefinition {
                id
                name
                displayName
                editorType
                parameters {
                  name
                  displayName
                  description
                  type
                }
            }
        }
    }
    ";

    public FieldDefinitionService(ITenantContext tenantContext, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger<FieldDefinitionService> logger)
        : base(tenantContext, authenticationStateProvider, httpClientFactory, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        return await ExecuteCommandAsync("field-definitions", command);
    }

    public async Task<IEnumerable<FieldDefinitionView>> GetFieldDefinitons()
    {
        return await QueryGraphAsync<FieldDefinitionView>("fieldDefinitions", DefaultQuery); ;
    }
}
