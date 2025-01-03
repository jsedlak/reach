﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.EditorApp.ServiceModel;
using Reach.Membership.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class EditorDefinitionService : BaseService
{
    private const string DefaultQuery = @"
    query {
        editorDefinitions {
            id
            organizationId
            hubId
            name
            editorType
            parameters {
              name
              displayName
              description
              type
            }
        }
    }
    ";

    public EditorDefinitionService(IOrganizationService organizationService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger<EditorDefinitionService> logger) 
        : base(organizationService, navigationManager, authenticationStateProvider, httpClientFactory, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        return await ExecuteCommandAsync("editor-definitions", command);
    }

    public async Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(string? query = null)
    {
        return await QueryGraphAsync<EditorDefinitionView>("editorDefinitions", query ?? DefaultQuery);
    }
}
