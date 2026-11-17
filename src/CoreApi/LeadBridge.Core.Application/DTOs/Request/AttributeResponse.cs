using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.DTOs.Request;

public sealed record class AttributeResponse(string Key, string Name, AttributeType Type, string? Unit, IReadOnlyList<string> AllowedValues);