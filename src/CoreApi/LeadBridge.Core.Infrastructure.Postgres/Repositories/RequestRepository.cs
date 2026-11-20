using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Core.Infrastructure.Postgres.Repositories;
public class RequestRepository : IRequestRepository
{
    private readonly ApplicationDbContext _context;
    public RequestRepository(ApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task<Result> AddAsync(Request request, CancellationToken cancellationToken = default)
    {                           
        _context.Requests.Add(request);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<IReadOnlyList<Request>> GetByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default)
    {
        return await _context.Requests
            .AsNoTracking()
            .Where(r => r.OwnerId == ownerId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Maybe<Request>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

        return Maybe.From(request);
    }
}