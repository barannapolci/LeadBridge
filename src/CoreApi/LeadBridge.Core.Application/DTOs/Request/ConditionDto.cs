using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.Enums;
using LeadBridge.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace LeadBridge.Core.Application.DTOs;
public class ConditionDto
{
    [Required, MaxLength(AttributeDefinition.MaxLengthOfKey)]
    public required string AttributeKey { get; init; }

    [Required, EnumDataType(typeof(AttributeType))]
    public required AttributeType Type { get; init; }

    public decimal? Min { get; init; }
    
    public decimal? Max { get; init; }
    [MaxLength(100)]
    public IReadOnlyList<string>? Values { get; init; }
}