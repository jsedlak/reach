using Orleans.Streams;
using Petl.EventSourcing;
using Reach.Content.Commands.Fields;
using Reach.Content.Events.Fields;
using Reach.Content.Model;
using Reach.Cqrs;

namespace Reach.Silo.Content.Grains;

public class FieldDefinitionGrain : EventSourcedGrain<FieldDefinition, BaseFieldDefinitionEvent>, IFieldDefinitionGrain
{
    private IAsyncStream<BaseFieldDefinitionEvent>? _eventStream;

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var streamProvider = this.GetStreamProvider("StreamProvider");

        var myId = this.GetGrainId().GetGuidKey();
        var streamId = StreamId.Create(GrainConstants.FieldDefinition_EventStream, myId);

        // grab a ref to the stream using the stream id
        _eventStream = streamProvider.GetStream<BaseFieldDefinitionEvent>(streamId);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override async Task Raise(BaseFieldDefinitionEvent @event)
    {
        await base.Raise(@event);

        if (_eventStream is not null)
        {
            await _eventStream.OnNextAsync(@event);
        }
    }

    protected override async Task Raise(IEnumerable<BaseFieldDefinitionEvent> events)
    {
        await base.Raise(events);
        
        if(_eventStream is not null)
        {
            await _eventStream.OnNextBatchAsync(events);
        }
    }

    public async Task<CommandResponse> Create(CreateFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionCreatedEvent(command.AggregateId)
        {
            Name = command.Name,
            Key = command.Name.ToSlug(),
            EditorDefinitionId = command.EditorDefinitionId,
            EditorParameters = new Dictionary<string, string>()
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorDefinition(SetFieldDefinitionEditorCommand command)
    {
        await Raise(new FieldDefinitionEditorSetEvent(command.AggregateId)
        {
            EditorDefinitionId = command.EditorDefinitionId,
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> SetEditorParameters(SetFieldDefinitionEditorParametersCommand command)
    {
        await Raise(new FieldDefinitionEditorParametersSetEvent(command.AggregateId)
        {
            EditorParameters = command.EditorParameters
        });

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }

    public async Task<CommandResponse> Delete(DeleteFieldDefinitionCommand command)
    {
        await Raise(new FieldDefinitionDeletedEvent(command.AggregateId));

        return new CommandResponse()
        {
            IsSuccess = true
        };
    }
}
