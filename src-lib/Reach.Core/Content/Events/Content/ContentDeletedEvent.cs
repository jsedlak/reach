namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentDeletedEvent : BaseContentEvent
{
    public ContentDeletedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }
}