using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.ComponentDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class ComponentDefinitionService : BaseContentService
{
    private const string Endpoint = "component-definitions";
    // private const string GraphEntity = "componentDefinitions";
    // private const string DefaultQuery = @"
    // query {
    //     componentDefinitions {
    //         id
    //         organizationId
    //         hubId
    //         name
    //         slug
    //         parentId
    //         fields {
    //             id
    //             name
    //             slug
    //             definitionId
    //             value
    //         }
    //     }
    // }";

    public ComponentDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger logger) 
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateComponentDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<ComponentDefinitionView>> GetComponentDefinitions(
        Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<ComponentDefinitionView>(organizationId, hubId, query: query);
    }
}