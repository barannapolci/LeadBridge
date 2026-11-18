using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Application.DTOs;
public class ConditionDto
{
    [Required]
    public required string AttributeKey { get; init; }

    public AttributeType Type { get; init; }

    public decimal? Min { get; init; }
    public decimal? Max { get; init; }
    public IReadOnlyList<string>? Values { get; init; }
}