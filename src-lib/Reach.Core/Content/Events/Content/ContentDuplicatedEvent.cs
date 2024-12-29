namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentDuplicatedEvent : BaseContentEvent
{
    public ContentDuplicatedEvent(Guid organizationId, Guid hubId, Guid aggregateId)
        : base(organizationId, hubId, aggregateId)
    {
    }

    [Id(0)]
    public Guid TargetHubId { get; set; }

    [Id(1)]
    public Guid TargetParentId { get; set; }
}