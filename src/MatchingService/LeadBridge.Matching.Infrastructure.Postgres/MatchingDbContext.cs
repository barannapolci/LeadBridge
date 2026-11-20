using LeadBridge.Matching.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Matching.Infrastructure.Postgres;
public class MatchingDbContext : DbContext
{
    public MatchingDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<IndexedItem> IndexedItems => Set<IndexedItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MatchingDbContext).Assembly);
    }
}