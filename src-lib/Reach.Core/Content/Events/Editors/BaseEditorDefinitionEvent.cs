using Reach.Cqrs;

namespace Reach.Content.Events.Editors;

[GenerateSerializer]
public abstract class BaseEditorDefinitionEvent : BaseEvent
{
    public BaseEditorDefinitionEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }
}
