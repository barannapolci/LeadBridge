using System.ComponentModel.DataAnnotations;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Antiforgery;

namespace LeadBridge.Core.Application.DTOs.Request;
public class CreateRequestItemDto
{   
    [Required, MaxLength(Category.MaxLengthOfCategoryKey)]
    public required string CategoryKey { get; init; }

    [Required, MaxLength(RequestItem.MaxConditions)]
    public required IReadOnlyList<ConditionDto> Conditions { get; init; }
}