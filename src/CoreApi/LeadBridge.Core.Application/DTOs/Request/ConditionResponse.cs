using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.DTOs.Request;
public sealed record ConditionResponse(
    string AttributeKey,
    AttributeType Type,
    decimal? Min,
    decimal? Max,
    IReadOnlyList<string> Values);