using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Matching.Infrastructure.Postgres;
public class MatchingDbContext : DbContext
{
    public MatchingDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}