using Microsoft.EntityFrameworkCore;
using Reach.Silo.Model;

namespace Reach.Silo.Data;

internal class MembershipDbContext : DbContext
{
    public MembershipDbContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Organization> Organizations { get; set; } = null!;

    public DbSet<Hub> Hubs { get; set; } = null!;
}
