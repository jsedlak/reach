namespace Reach.Silo.Membership.Model;

[GenerateSerializer]
internal class Account
{
    [Id(0)]
    public AccountSettings Settings { get; set; } = new();
}
