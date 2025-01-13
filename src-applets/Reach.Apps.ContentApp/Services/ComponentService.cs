using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.Components;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class ComponentService : BaseContentService
{
    private const string Endpoint = "components";
    // private const string GraphEntity = "components";
    // private const string DefaultQuery = @"
    // query {
    //     components {
    //         id
    //         organizationId
    //         hubId
    //         name
    //         definitionId
    //         fields {
    //             id
    //             name
    //             slug
    //             definitionId
    //             value
    //         }
    //     }
    // }";

    public ComponentService(IGraphClient graphClient, ICommandClient commandClient, ILogger<ComponentService> logger) 
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentView>> GetComponents(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<ComponentView>(organizationId, hubId, query: query);
    }
}