namespace Reach.Cqrs;

/// <summary>
/// Provides a globally unique identifier for any resource on the platform, mainly used to connect to Orleans grains.
/// </summary>
public readonly record struct ResourceId
{
    public static ResourceId Parse(string resourceId)
    {
        var split = resourceId.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

        if (split.Length < 3)
        {
            throw new InvalidDataException("Aggregate Id was not well formed. Aggregate Ids should be in the form Organization/Hub/Resource");
        }

        if (!Guid.TryParse(split[0], out var organizationId))
        {
            throw new InvalidDataException("Organization Id was not a well formed GUID.");
        }

        if (!Guid.TryParse(split[1], out var hubId))
        {
            throw new InvalidDataException("Hub Id was not a well formed GUID.");
        }

        if (!Guid.TryParse(split[2], out var aggregateId))
        {
            throw new InvalidDataException("Aggregate Id was not a well formed GUID.");
        }

        return new ResourceId
        {
            OrganizationId = organizationId,
            HubId = hubId,
            AggregateId = aggregateId
        };
    }

    public ResourceId()
    {

    }

    public ResourceId(Guid organizationId, Guid hubId, Guid aggregateId)
    {
        OrganizationId = organizationId;
        HubId = hubId;
        AggregateId = aggregateId;
    }

    /// <summary>
    /// Gets or Sets the unique identifier of the organization the resource belongs to
    /// </summary>
    public Guid OrganizationId { get; init; }

    /// <summary>
    /// Gets or Sets the unique identifier of the hub the resource belongs to
    /// </summary>
    public Guid HubId { get; init; } 

    /// <summary>
    /// Gets or Sets the unique identifier of the resource being interacted with
    /// </summary>
    public Guid AggregateId { get; init; }

    public static implicit operator string(ResourceId aggregateId) => aggregateId.ToString();

    public override string ToString()
    {
        return $"{OrganizationId}/{HubId}/{AggregateId}";
    }
}