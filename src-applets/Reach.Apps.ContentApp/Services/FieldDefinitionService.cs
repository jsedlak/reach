﻿
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Components.Context;
using Reach.Content.Commands.Fields;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.EditorApp.ServiceModel;

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

    public FieldDefinitionService(ITenantService tenantService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger<FieldDefinitionService> logger)
        : base(tenantService, navigationManager, authenticationStateProvider, httpClientFactory, logger)
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
