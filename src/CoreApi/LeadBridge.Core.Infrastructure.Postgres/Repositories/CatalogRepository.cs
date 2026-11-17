using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Core.Infrastructure.Postgres.Repositories;
public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationDbContext _context;

    public CatalogRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Maybe<Category>> FindByKeyWithAttributesAsync(string key, CancellationToken cancellationToken = default)
    {
        var category = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Attributes)
            .FirstOrDefaultAsync(c => c.Key == key, cancellationToken);

        return Maybe.From(category);
    }
}