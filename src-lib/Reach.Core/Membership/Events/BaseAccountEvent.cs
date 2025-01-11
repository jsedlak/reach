namespace Reach.Membership.Events;

[GenerateSerializer]
public abstract class BaseAccountEvent
{
    protected BaseAccountEvent(string accountId)
    {
        AccountId = accountId;
    }

    [Id(0)]
    public string AccountId { get; set; } = null!;
}
