namespace ReachCms.Domains.Content.Commands;

public sealed class CreateContentCommand
{
    public Guid ParentId { get; set; }

    public Guid TemplateId { get; set; }

    public string Name { get; set; } = null!;
}
