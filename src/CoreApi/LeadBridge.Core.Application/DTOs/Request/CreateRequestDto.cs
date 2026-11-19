using System.ComponentModel.DataAnnotations;
using RequestEntity = LeadBridge.Core.Domain.Entities.Request;

namespace LeadBridge.Core.Application.DTOs.Request;
public class CreateRequestDto
{
    [Required, MinLength(1), MaxLength(RequestEntity.MaxItems)]
    public required IReadOnlyList<CreateRequestItemDto> Items { get; init; }
}