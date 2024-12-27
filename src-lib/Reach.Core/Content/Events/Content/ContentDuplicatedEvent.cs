namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentDuplicatedEvent : BaseContentEvent
{
    public ContentDuplicatedEvent(Guid aggregateId, Guid organizationId, Guid hubId) 
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)]
    public Guid TargetHubId { get; set; }

    [Id(1)]
    public Guid TargetParentId { get; set; }
}