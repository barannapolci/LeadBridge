using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.DTOs.Request;
public sealed record RequestResponse(
    Guid Id,
    RequestSide Side,
    RequestStatus Status,
    int Version,
    DateTime CreatedAt,
    IReadOnlyList<RequestItemResponse> Items);