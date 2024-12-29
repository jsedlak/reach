using Microsoft.EntityFrameworkCore;
using Reach.Membership.Postgres.Model;

namespace Reach.Membership.Postgres.Data;

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
