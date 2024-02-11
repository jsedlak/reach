namespace ReachCms.Domains.Content.Model;

public class FileItem : IContentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ParentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = null!;

}