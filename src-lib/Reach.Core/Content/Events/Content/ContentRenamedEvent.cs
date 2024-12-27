namespace Reach.Content.Events.Content;

[GenerateSerializer]
public class ContentRenamedEvent : BaseContentEvent
{
    public ContentRenamedEvent(Guid aggregateId, Guid organizationId, Guid hubId)
        : base(aggregateId, organizationId, hubId)
    {
    }

    [Id(0)] public string Name { get; set; } = null!;

    [Id(1)] public string Slug { get; set; } = null!;
}