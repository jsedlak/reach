namespace ReachCms.Domains.Content.Model;

/// <summary>
/// A document describing how a particular piece of content should be constructed and what data it contains
/// </summary>
public class TemplateItem : IContentEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ParentId { get; set; } = Guid.Empty;

    public string Name { get; set; } = string.Empty;

    public IEnumerable<ITemplatePart> Parts { get; set; } = Enumerable.Empty<ITemplatePart>();
}
