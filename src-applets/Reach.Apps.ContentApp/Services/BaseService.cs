using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using Reach.Cqrs;
using Reach.Membership;
using Reach.Membership.Views;
using Reach.Platform.ServiceModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Reach.Platform.Json.Converters;

namespace Reach.Apps.ContentApp.Services;

public abstract class BaseContentService
{
    protected BaseContentService(IGraphClient graphClient, ICommandClient commandClient, ILogger logger)
    {
        Logger = logger;
        GraphClient = graphClient;
        CommandClient = commandClient;
    }

    protected IGraphClient GraphClient { get; }

    protected ICommandClient CommandClient { get; }
    
    protected ILogger Logger { get; }
}

