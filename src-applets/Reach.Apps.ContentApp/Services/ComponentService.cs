using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Commands.Components;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Membership.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class ComponentService : BaseService
{
    private const string Endpoint = "components";
    private const string GraphEntity = "components";
    private const string DefaultQuery = @"
    query {
        components {
            id
            organizationId
            hubId
            name
            definitionId
            fields {
                id
                name
                slug
                definitionId
                value
            }
        }
    }";

    public ComponentService(IOrganizationService organizationService, NavigationManager navigationManager, AuthenticationStateProvider authenticationStateProvider, IHttpClientFactory httpClientFactory, ILogger logger) 
        : base(organizationService, navigationManager, authenticationStateProvider, httpClientFactory, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentCommand command)
    {
        return await ExecuteCommandAsync(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentView>> GetComponents(string? query = null)
    {
        return await QueryGraphAsync<ComponentView>(GraphEntity, query ?? DefaultQuery);
    }
}