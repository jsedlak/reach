namespace Reach.Membership.Commands;

/// <summary>
/// Enables or Disables the onboarding procedure for a user
/// </summary>
[GenerateSerializer]
public class SetSkipOnboardingFlagCommand
{
    /// <summary>
    /// Gets or Sets whether onboarding should be skipped for a user
    /// </summary>
    [Id(0)]
    public bool SkipOnboarding { get; set; }
}
