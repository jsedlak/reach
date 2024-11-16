using Microsoft.Extensions.Logging;
using Orleans.Streams;
using Orleans.Streams.Core;

namespace Reach.Silo.Content.Grains;

public abstract class SubscribedViewGrain<TEvent> : Grain, IStreamSubscriptionObserver, IAsyncObserver<TEvent>
{
    private readonly ILogger _logger;

    protected SubscribedViewGrain(ILogger logger)
    {
        _logger = logger;
    }

    #region Implicit Subscription Management
    public async Task OnSubscribed(IStreamSubscriptionHandleFactory handleFactory)
    {
        var handle = handleFactory.Create<TEvent>();
        await handle.ResumeAsync(this);
    }

    public async Task OnNextAsync(TEvent item, StreamSequenceToken? token = null)
    {
        _logger.LogInformation($"Captured event: {item.GetType().Name}");

        try
        {
            dynamic o = this;
            dynamic e = item;
            await o.Handle(e);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Could not handle event {item.GetType().Name}");
        }
    }

    public Task OnCompletedAsync()
    {
        _logger.LogInformation("OnCompletedAsync");
        return Task.CompletedTask;
    }

    public Task OnErrorAsync(Exception ex)
    {
        _logger.LogInformation("OnErrorAsync");
        return Task.CompletedTask;
    }
    #endregion
}
