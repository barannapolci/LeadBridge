using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.Interfaces.Services;
public interface IRequestService
{
    Task<Result<Guid>> CreateAsync(Guid userId, RequestSide side, CreateRequestDto requestDto, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RequestResponse>> GetOwnRequestsAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<Result<RequestResponse>> GetOwnRequestAsync(Guid userId, Guid requestId, CancellationToken cancellationToken = default);
}