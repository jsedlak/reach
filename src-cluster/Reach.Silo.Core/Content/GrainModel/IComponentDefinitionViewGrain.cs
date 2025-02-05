using Reach.Content.Events.ComponentDefinitions;

namespace Reach.Silo.Content.GrainModel;

public interface IComponentDefinitionViewGrain : IGrainWithStringKey
{
    Task Handle(ComponentDefinitionCreatedEvent @event);

    Task Handle(ComponentDefinitionRenamedEvent @event);

    Task Handle(ComponentDefinitionRendererSetEvent @event);

    Task Handle(FieldAddedToComponentDefinitionEvent @event);

    Task Handle(FieldRemovedFromComponentDefinitionEvent @event);

    Task Handle(ComponentDefinitionDeletedEvent @event);
}