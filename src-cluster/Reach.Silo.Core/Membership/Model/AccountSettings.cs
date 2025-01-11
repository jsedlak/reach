namespace Reach.Silo.Membership.Model;

[GenerateSerializer]
internal class AccountSettings
{
    [Id(0)]
    public bool SkipOnboarding { get; set; } = false;
}
