using Reach.Content.Events.Components;

namespace Reach.Silo.Content.GrainModel;

public interface IComponentViewGrain : IGrainWithStringKey
{
    Task Handle(ComponentCreatedEvent @event);

    Task Handle(ComponentRenamedEvent @event);

    Task Handle(ComponentFieldValueSetEvent @event);

    Task Handle(ComponentDeletedEvent @event);
}