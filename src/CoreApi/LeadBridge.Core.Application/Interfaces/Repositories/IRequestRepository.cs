using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Entities;
namespace LeadBridge.Core.Application.Interfaces;
public interface IRequestRepository
{
    Task<Result> AddAsync(Request request, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Request>> GetByOwnerAsync(Guid ownerId, CancellationToken cancellationToken = default);

    Task<Maybe<Request>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}