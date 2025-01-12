using System.ComponentModel.DataAnnotations;

namespace Reach.Silo.Model;

internal class Hub
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
    public string IconUrl { get; set; } = string.Empty;

    public Guid OrganizationId { get; set; } = Guid.Empty;
}