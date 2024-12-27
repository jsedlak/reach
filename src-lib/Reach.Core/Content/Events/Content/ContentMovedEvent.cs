namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentMovedEvent : BaseContentEvent
{
    public ContentMovedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }
    
    [Id(0)]
    public Guid TargetHubId { get; set; }

    [Id(1)]
    public Guid TargetParentId { get; set; }
}