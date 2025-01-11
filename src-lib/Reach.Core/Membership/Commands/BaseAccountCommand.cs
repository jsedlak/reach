namespace Reach.Membership.Commands;

[GenerateSerializer]
public class BaseAccountCommand
{
    /// <summary>
    /// Gets or Sets the account id for the command
    /// </summary>
    [Id(0)]
    public string AccountId { get; set; } = null!;
}