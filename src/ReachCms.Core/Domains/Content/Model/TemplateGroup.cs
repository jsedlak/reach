namespace ReachCms.Domains.Content.Model;

public class TemplateGroup : ITemplatePart
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = null!;

    public IEnumerable<ITemplatePart> Children { get; set; } = Enumerable.Empty<ITemplatePart>();
}
