namespace Reach.Membership.Events;

public class SkipOnboardingFlagSetEvent : BaseAccountEvent
{
    public SkipOnboardingFlagSetEvent(string accountId) 
        : base(accountId)
    {
    }

    public bool SkipOnboarding { get; set; }
}
