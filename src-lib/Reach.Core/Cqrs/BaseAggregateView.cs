namespace Reach.Cqrs;

public abstract class BaseAggregateView : IAggregateView
{
    /// <summary>
    /// Gets or Sets the unique identifier for the aggregate view represents
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Gets or Sets the organization in which this view belongs
    /// </summary>
    public Guid OrganizationId { get; set; }
    
    /// <summary>
    /// Gets or Sets the hub in which this view belongs
    /// </summary>
    public Guid HubId { get; set; }
}