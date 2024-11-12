using Orleans.Streams.Core;
using Orleans.Streams;
using Reach.Content.Events.Fields;
using Microsoft.Extensions.Logging;
using Reach.Content.ServiceModel;
using Reach.Silo.Content.ServiceModel;

namespace Reach.Silo.Content.Grains;

[ImplicitStreamSubscription(GrainConstants.FieldDefinition_EventStream)]
public class FieldDefinitionViewGrain : 
    Grain, 
    IFieldDefinitionViewGrain, 
    IStreamSubscriptionObserver, 
    IAsyncObserver<BaseFieldDefinitionEvent>
{
    private readonly ILogger<FieldDefinitionViewGrain> _logger;
    private readonly IFieldDefinitionViewReadRepository _fieldDefinitionReadRepository;
    private readonly IFieldDefinitionViewWriteRepository _fieldDefinitionViewWriteRepository;
    private readonly IEditorDefinitionViewReadRepository _editorDefinitionReadRepository;

    public FieldDefinitionViewGrain(
        IFieldDefinitionViewReadRepository fieldDefinitionReadRepository,
        IFieldDefinitionViewWriteRepository fieldDefinitionViewWriteRepository,
        IEditorDefinitionViewReadRepository editorDefinitionReadRepository, 
        ILogger<FieldDefinitionViewGrain> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fieldDefinitionReadRepository = fieldDefinitionReadRepository;
        _fieldDefinitionViewWriteRepository = fieldDefinitionViewWriteRepository;
        _editorDefinitionReadRepository = editorDefinitionReadRepository;
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

    private async Task Handle(FieldDefinitionCreatedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionCreatedEvent)} handled on Grain ID {this.GetGrainId().GetGuidKey()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if(result is null)
        {
            result = new Reach.Content.Views.FieldDefinitionView()
            {
                Id = @event.AggregateId
            };
        }

        result.Name = @event.Name;
        result.Key = @event.Key;
        result.EditorDefinitionId = @event.EditorDefinitionId;
        result.EditorDefinition = await _editorDefinitionReadRepository.Get(@event.EditorDefinitionId);
        result.EditorParameters = @event.EditorParameters;

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }

    private async Task Handle(FieldDefinitionDeletedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionDeletedEvent)} handled on Grain ID {this.GetGrainId().GetGuidKey()}.");

        await _fieldDefinitionViewWriteRepository.Delete(@event.AggregateId);
    }

    private async Task Handle(FieldDefinitionEditorSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorSetEvent)} handled on Grain ID {this.GetGrainId().GetGuidKey()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if(result is null)
        {
            // TODO: Do what?
            return;
        }

        result.EditorDefinitionId = @event.EditorDefinitionId;
        result.EditorDefinition = await _editorDefinitionReadRepository.Get(@event.EditorDefinitionId);

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }

    private async Task Handle(FieldDefinitionEditorParametersSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorParametersSetEvent)} handled on Grain ID {this.GetGrainId().GetGuidKey()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if (result is null)
        {
            // TODO: Do what?
            return;
        }

        result.EditorParameters = @event.EditorParameters;

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }
}
