namespace ReachCms.Domains.Content.Model;

/// <summary>
/// A document describing a piece of content adhering to a particular structure of data (template)
/// </summary>
public class ContentItem : IContentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ParentId { get; set; } = Guid.Empty;

    public Guid TemplateId { get; set; }

    public string Name { get; set; } = null!;
}
