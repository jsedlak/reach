namespace ReachCms.Domains.Content.Commands;

public sealed class RenameContentCommand
{
    public Guid AggregateId { get; set; }

    public string Name { get; set; } = null!;
}