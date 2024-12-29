namespace Reach.Cqrs;

public interface IAggregateView : IView
{
    public Guid OrganizationId { get; set; }

    public Guid HubId { get; set; }
}