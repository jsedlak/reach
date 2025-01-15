using Microsoft.Extensions.Logging;
using Reach.Platform.ServiceModel;

namespace Reach.Platform.Services;

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

