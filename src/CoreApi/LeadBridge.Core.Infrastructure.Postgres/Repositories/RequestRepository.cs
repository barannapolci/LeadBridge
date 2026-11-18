using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Domain.Entities;

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
}