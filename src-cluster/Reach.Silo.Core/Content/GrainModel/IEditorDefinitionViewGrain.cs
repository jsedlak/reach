using Reach.Content.Events.EditorDefinitions;

namespace Reach.Silo.Content.GrainModel;

public interface IEditorDefinitionViewGrain : IGrainWithStringKey
{
    Task Handle(EditorDefinitionCreatedEvent @event);

    Task Handle(EditorDefinitionParametersSetEvent @event);

    Task Handle(EditorDefinitionDeletedEvent @event);
}