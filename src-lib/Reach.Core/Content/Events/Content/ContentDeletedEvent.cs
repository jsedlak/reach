namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentDeletedEvent : BaseContentEvent
{
    public ContentDeletedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
}