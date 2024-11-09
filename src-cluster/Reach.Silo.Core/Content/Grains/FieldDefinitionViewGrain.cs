using Orleans.Streams.Core;
using Orleans.Streams;
using Reach.Content.Events.Fields;
using Microsoft.Extensions.Logging;

namespace Reach.Silo.Content.Grains;

[ImplicitStreamSubscription(GrainConstants.FieldDefinition_EventStream)]
public class FieldDefinitionViewGrain : 
    Grain, 
    IFieldDefinitionViewGrain, 
    IStreamSubscriptionObserver, 
    IAsyncObserver<BaseFieldDefinitionEvent>
{
    private readonly ILogger<FieldDefinitionViewGrain> _logger;

    public FieldDefinitionViewGrain(ILogger<FieldDefinitionViewGrain> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    #region Implicit Subscription Management
    public async Task OnSubscribed(IStreamSubscriptionHandleFactory handleFactory)
    {
        var handle = handleFactory.Create<BaseFieldDefinitionEvent>();
        await handle.ResumeAsync(this);
    }

    public async Task OnNextAsync(BaseFieldDefinitionEvent item, StreamSequenceToken? token = null)
    {
        _logger.LogInformation("OnNextAsync");

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

    private Task Handle(FieldDefinitionCreatedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionCreatedEvent)} handled.");
        return Task.CompletedTask;
    }

    private Task Handle(FieldDefinitionDeletedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionDeletedEvent)} handled.");
        return Task.CompletedTask;
    }

    private Task Handle(FieldDefinitionEditorSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorSetEvent)} handled.");
        return Task.CompletedTask;
    }

    private Task Handle(FieldDefinitionEditorParametersSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorParametersSetEvent)} handled.");
        return Task.CompletedTask;
    }
}
