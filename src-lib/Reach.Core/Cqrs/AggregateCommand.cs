﻿namespace Reach.Cqrs;

/// <summary>
/// Provides a base implementation for all commands going against an aggregate root
/// </summary>
[GenerateSerializer]
public abstract class AggregateCommand
{
    protected AggregateCommand(ResourceId resourceId)
        : this(resourceId.OrganizationId, resourceId.HubId, resourceId.AggregateId)
    {
    }

    protected AggregateCommand(Guid organizationId, Guid hubId, Guid aggregateId)
    {
        AggregateId = aggregateId;
        OrganizationId = organizationId;
        HubId = hubId;
    }

    /// <summary>
    /// The unique identifier of the aggregate root being targeted
    /// </summary>
    [Id(0)]
    public Guid AggregateId { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or Sets the unique identifier of the organization to which the resource belongs
    /// </summary>
    [Id(1)]
    public Guid OrganizationId { get; set; } = Guid.Empty;

    /// <summary>
    /// Gets or Sets the unique identifier of the hub to which the resource belongs
    /// </summary>
    [Id(2)]
    public Guid HubId { get; set; } = Guid.Empty;
}
