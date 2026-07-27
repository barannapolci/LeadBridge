namespace LeadBridge.Core.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions option)
        : base(option)
    {
    }
}
