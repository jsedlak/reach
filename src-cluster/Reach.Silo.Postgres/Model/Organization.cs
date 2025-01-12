using System.ComponentModel.DataAnnotations;

namespace Reach.Silo.Model;

internal class Organization
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public string Slug { get; set; } = null!;

    [Required]
    public string OwnerIdentifier { get; set; } = null!;

    public virtual ICollection<Hub> Hubs { get; set; } = [];
}
