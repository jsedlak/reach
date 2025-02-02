using Reach.Content.Events.FieldDefinitions;

namespace Reach.Silo.Content.GrainModel;

public interface IFieldDefinitionViewGrain : IGrainWithStringKey
{
    Task Handle(FieldDefinitionCreatedEvent @event);

    Task Handle(FieldDefinitionDeletedEvent @event);

    Task Handle(FieldDefinitionEditorParametersSetEvent @event);

    Task Handle(FieldDefinitionEditorSetEvent @event);
}
