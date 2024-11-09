using Orleans.Streams;
using Petl.EventSourcing;
using Reach.Content.Events.Fields;

namespace Reach.Silo.Content.Grains;

public abstract class StreamingEventSourcedGrain<TState, TEvent> : EventSourcedGrain<TState, TEvent>
    where TState : class, new()
    where TEvent : class
{
    private readonly string _streamId = null!;
    private IAsyncStream<TEvent>? _eventStream;

    protected StreamingEventSourcedGrain(string streamId)
    {
        _streamId = streamId;
    }

    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        var streamProvider = this.GetStreamProvider("StreamProvider");

        var myId = this.GetGrainId().GetGuidKey();
        var streamId = StreamId.Create(_streamId, myId);

        // grab a ref to the stream using the stream id
        _eventStream = streamProvider.GetStream<TEvent>(streamId);

        return base.OnActivateAsync(cancellationToken);
    }

    protected override async Task Raise(TEvent @event)
    {
        await base.Raise(@event);

        if (_eventStream is not null)
        {
            await _eventStream.OnNextAsync(@event);
        }
    }

    protected override async Task Raise(IEnumerable<TEvent> events)
    {
        await base.Raise(events);

        if (_eventStream is not null)
        {
            await _eventStream.OnNextBatchAsync(events);
        }
    }
}