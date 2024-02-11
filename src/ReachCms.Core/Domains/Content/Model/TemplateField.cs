namespace ReachCms.Domains.Content.Model;

public class TemplateField : ITemplatePart
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IEnumerable<ITemplatePart> Children { get; set; } = Enumerable.Empty<ITemplatePart>();
}