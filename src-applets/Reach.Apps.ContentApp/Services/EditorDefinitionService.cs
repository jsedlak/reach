using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.EditorDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class EditorDefinitionService : BaseContentService
{
    private const string Endpoint = "editor-definitions";
    // private const string DefaultQuery = @"
    // query {
    //     editorDefinitions {
    //         id
    //         organizationId
    //         hubId
    //         name
    //         editorType
    //         parameters {
    //           name
    //           displayName
    //           description
    //           type
    //         }
    //     }
    // }
    // ";

    public EditorDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger logger) 
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateEditorDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<EditorDefinitionView>> GetEditorDefinitions(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<EditorDefinitionView>(organizationId, hubId, query: query);
    }
}