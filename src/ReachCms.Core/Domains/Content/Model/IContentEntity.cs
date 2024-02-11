namespace ReachCms.Domains.Content.Model;

public interface IContentEntity
{
    Guid Id { get; set; }

    Guid ParentId { get; set; }

    string Name { get; set; }
}
