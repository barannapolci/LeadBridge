namespace LeadBridge.Core.Application.DTOs.Request;
public sealed record RequestItemResponse(
    string CategoryKey,
    IReadOnlyList<ConditionResponse> Conditions);