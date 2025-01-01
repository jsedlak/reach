using Reach.Cqrs;

namespace Reach.Content.Events.EditorDefinitions;

[GenerateSerializer]
public abstract class BaseEditorDefinitionEvent : BaseEvent
{
    public BaseEditorDefinitionEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}
