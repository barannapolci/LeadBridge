using System.Security.Claims;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces.Services;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadBridge.Core.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/request")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreatedRequestResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CreatedRequestResponse>> Create(CreateRequestDto dto, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
        {
            return Unauthorized();
        }

        if (!Enum.TryParse<RequestSide>(User.FindFirstValue("role"), out var side))
        {
            return Unauthorized();
        }

        var requestId = await _requestService.CreateAsync(userId, side, dto, cancellationToken);

        if (requestId.IsFailure)
        {
            return BadRequest(requestId.Error);
        }

        var response = new CreatedRequestResponse(requestId.Value);

        return Created($"/api/request/{response.Id}", response);   
    }

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<RequestResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]

    public async Task<ActionResult<IReadOnlyList<RequestResponse>>> GetMine(CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
        {
            return Unauthorized();
        }

        return Ok(await _requestService.GetOwnRequestsAsync(userId, cancellationToken));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(RequestResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<RequestResponse>> GetOneMine(Guid id, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(User.FindFirstValue("sub"), out var userId))
        {
            return Unauthorized();
        }

        var requestResult = await _requestService.GetOwnRequestAsync(userId, id, cancellationToken);

        if (requestResult.IsFailure)
        {
            return NotFound(requestResult.Error);
        }

        return Ok(requestResult.Value);
    }
}