
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Content.Commands.FieldDefinitions;
using Reach.Content.Views;
using Reach.Cqrs;
using Reach.Platform.ServiceModel;

namespace Reach.Apps.ContentApp.Services;

public class FieldDefinitionService : BaseContentService
{
    private const string Endpoint = "component-definitions";
    
    // private const string DefaultQuery = @"
    // query {
    //     fieldDefinitions {
    //         id
    //         name
    //         organizationId
    //         hubId
    //         key
    //         editorDefinitionId
    //         editorParameters {
    //             key
    //             value
    //         }
    //         editorDefinition {
    //             id
    //             name
    //             organizationId
    //             hubId
    //             displayName
    //             editorType
    //             parameters {
    //               name
    //               displayName
    //               description
    //               type
    //             }
    //         }
    //     }
    // }
    // ";

    public FieldDefinitionService(IGraphClient graphClient, ICommandClient commandClient, ILogger logger) 
        : base(graphClient, commandClient, logger)
    {
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        return await CommandClient.Execute(Endpoint, command);
    }

    public async Task<IEnumerable<FieldDefinitionView>> GetFieldDefinitons(Guid organizationId, Guid hubId, string? query = null)
    {
        return await GraphClient.GetMany<FieldDefinitionView>(organizationId, hubId, query: query);
    }
}
