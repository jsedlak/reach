using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class ComponentDefinitionService : BaseService
{
    private const string Endpoint = "component-definitions";
    private const string GraphEntity = "componentDefinitions";
    private const string DefaultQuery = @"
    query {
        componentDefinitions {
            id
            organizationId
            hubId
            name
            slug
            parentId
            fields {
                id
                name
                slug
                definitionId
                value
            }
        }
    }";

    public ComponentDefinitionService(IOrganizationService organizationService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger logger) 
        : base(organizationService, navigationManager, authenticationStateProvider, httpClientFactory, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentDefinitionCommand command)
    {
        return await ExecuteCommandAsync(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentDefinitionView>> GetComponentDefinitions(string? query = null)
    {
        return await QueryGraphAsync<ComponentDefinitionView>(GraphEntity, query ?? DefaultQuery);
    }
}