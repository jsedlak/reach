using Reach.Content.Events.Fields;
using Microsoft.Extensions.Logging;
using Reach.Content.ServiceModel;
using Reach.Silo.Content.ServiceModel;
using Reach.Silo.Content.GrainModel;

namespace Reach.Silo.Content.Grains.FieldDefinitions;

[ImplicitStreamSubscription(GrainConstants.FieldDefinition_EventStream)]
public class FieldDefinitionViewGrain : SubscribedViewGrain<BaseFieldDefinitionEvent>, IFieldDefinitionViewGrain
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
        : base(logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _fieldDefinitionReadRepository = fieldDefinitionReadRepository;
        _fieldDefinitionViewWriteRepository = fieldDefinitionViewWriteRepository;
        _editorDefinitionReadRepository = editorDefinitionReadRepository;
    }

    public async Task Handle(FieldDefinitionCreatedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionCreatedEvent)} handled on Grain ID {this.GetGrainId()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if (result is null)
        {
            result = new Reach.Content.Views.FieldDefinitionView()
            {
                Id = @event.AggregateId,
                OrganizationId = @event.OrganizationId,
                HubId = @event.HubId
            };
        }

        result.Name = @event.Name;
        result.Key = @event.Key;
        result.EditorDefinitionId = @event.EditorDefinitionId;
        result.EditorDefinition = await _editorDefinitionReadRepository.Get(@event.EditorDefinitionId);
        result.EditorParameters = @event.EditorParameters;

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(FieldDefinitionDeletedEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionDeletedEvent)} handled on Grain ID {this.GetGrainId()}.");

        await _fieldDefinitionViewWriteRepository.Delete(@event.AggregateId);
    }

    public async Task Handle(FieldDefinitionEditorSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorSetEvent)} handled on Grain ID {this.GetGrainId()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if (result is null)
        {
            throw new InvalidOperationException();
        }

        result.EditorDefinitionId = @event.EditorDefinitionId;
        result.EditorDefinition = await _editorDefinitionReadRepository.Get(@event.EditorDefinitionId);

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }

    public async Task Handle(FieldDefinitionEditorParametersSetEvent @event)
    {
        _logger.LogInformation($"{nameof(FieldDefinitionEditorParametersSetEvent)} handled on Grain ID {this.GetGrainId()}.");

        var result = await _fieldDefinitionReadRepository.Get(@event.AggregateId);

        if (result is null)
        {
            throw new InvalidOperationException();
        }

        result.EditorParameters = @event.EditorParameters;

        await _fieldDefinitionViewWriteRepository.Upsert(result);
    }
}
