namespace ReachCms.Domains.Content.Model;

public interface ITemplatePart
{
    Guid Id { get; set; }

    IEnumerable<ITemplatePart> Children { get; }
}
