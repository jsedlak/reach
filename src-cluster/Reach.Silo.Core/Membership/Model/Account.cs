using Reach.Membership.Events;

namespace Reach.Silo.Membership.Model;

[GenerateSerializer]
public class Account
{
    public void Apply(SkipOnboardingFlagSetEvent @event)
    {
        Settings.SkipOnboarding = @event.SkipOnboarding;
    }

    [Id(0)]
    public AccountSettings Settings { get; set; } = new();
}
