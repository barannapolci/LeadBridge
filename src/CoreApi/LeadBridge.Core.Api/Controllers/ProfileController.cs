using System.Security.Claims;
using LeadBridge.Core.Application.DTOs.Profile;
using LeadBridge.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace LeadBridge.Core.Api.Controllers;

[ApiController]
[Route("api")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ProfileResponse>> ProfileAsync(CancellationToken cancellationToken)
    {   
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
        {
            return Unauthorized();
        }

        var result = await _profileService.GetAsync(userId, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfileAsync(UpdateProfileRequest updateProfileRequest, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
        {
            return Unauthorized();
        }

        var result = await _profileService.ChangeAsync(userId, updateProfileRequest, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return NoContent();
    }
}