namespace Reach.Membership.Events;

[GenerateSerializer]
public class SkipOnboardingFlagSetEvent : BaseAccountEvent
{
    public SkipOnboardingFlagSetEvent(string accountId) 
        : base(accountId)
    {
    }

    [Id(0)]
    public bool SkipOnboarding { get; set; }
}
