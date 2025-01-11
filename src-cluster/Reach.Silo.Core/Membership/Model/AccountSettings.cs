namespace Reach.Silo.Membership.Model;

[GenerateSerializer]
public class AccountSettings
{
    [Id(0)]
    public bool SkipOnboarding { get; set; } = false;
}
