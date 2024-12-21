using System.ComponentModel.DataAnnotations;

namespace Reach.Membership.Postgres.Model;

public class Hub
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Slug { get; set; } = null!;

    [Required]
    public string OwnerIdentifier { get; set; } = null!;

    [Required]
    public string RegionKey { get; set; } = string.Empty;

    [Required]
    public string IconUrl { get; set; } = string.Empty;

    public Guid OrganizationId { get; set; } = Guid.Empty;
}