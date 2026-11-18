using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Antiforgery;

namespace LeadBridge.Core.Application.DTOs.Request;
public class CreateRequestItemDto
{   
    [Required]
    public required string CategoryKey { get; init; }

    public required IReadOnlyList<ConditionDto> Conditions { get; init; }
}