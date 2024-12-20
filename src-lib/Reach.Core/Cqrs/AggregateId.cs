namespace Reach.Cqrs;

/// <summary>
/// Provides a globally unique identifier for any resource on the platform, mainly used to connect to Orleans grains.
/// </summary>
public readonly record struct AggregateId
{
    public static AggregateId Parse(string aggregateId)
    {
        var split = aggregateId.Split(["/"], StringSplitOptions.RemoveEmptyEntries);

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

        if (!Guid.TryParse(split[2], out var resourceId))
        {
            throw new InvalidDataException("Resource Id was not a well formed GUID.");
        }

        return new AggregateId
        {
            OrganizationId = organizationId,
            HubId = hubId,
            ResourceId = resourceId
        };
    }

    public AggregateId()
    {

    }

    public AggregateId(Guid organizationId, Guid hubId, Guid resourceId)
    {
        OrganizationId = organizationId;
        HubId = hubId;
        ResourceId = resourceId;
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
    public Guid ResourceId { get; init; }

    public static implicit operator string(AggregateId aggregateId) => aggregateId.ToString();

    public override string ToString()
    {
        return $"{OrganizationId}/{HubId}/{ResourceId}";
    }
}