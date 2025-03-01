using Microsoft.EntityFrameworkCore;

namespace Reach.Silo.Content.Data;

internal class ContentDbContext : DbContext
{
    public ContentDbContext(DbContextOptions options)
        : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
