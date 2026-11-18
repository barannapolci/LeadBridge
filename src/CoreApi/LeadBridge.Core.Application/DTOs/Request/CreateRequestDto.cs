using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LeadBridge.Core.Application.DTOs.Request;
public class CreateRequestDto
{
    public required IReadOnlyList<CreateRequestItemDto> Items { get; init; }
}