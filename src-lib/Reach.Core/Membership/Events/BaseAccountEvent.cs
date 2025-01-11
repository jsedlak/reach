namespace Reach.Membership.Events;

public abstract class BaseAccountEvent
{
    protected BaseAccountEvent(string accountId)
    {
        AccountId = accountId;
    }

    public string AccountId { get; set; } = null!;
}
