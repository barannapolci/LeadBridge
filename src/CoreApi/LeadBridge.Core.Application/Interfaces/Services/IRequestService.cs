using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.Interfaces.Services;
public interface IRequestService
{
    Task<Result> Create(Guid userId, RequestSide side, CreateRequestDto requestDto, CancellationToken cancellationToken);    
}