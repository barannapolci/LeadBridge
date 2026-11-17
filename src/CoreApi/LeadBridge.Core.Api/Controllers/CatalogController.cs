using System.ComponentModel;
using System.Net.Http.Headers;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeadBridge.Core.Api.Controllers;

[ApiController]
[Route("api/catalog")]
[Authorize]
public class CatalogController : ControllerBase
{
    private readonly ICatalogService _catalogService;
    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> Get(CancellationToken cancellationToken)
    {
        return Ok(await _catalogService.GetAsync(cancellationToken));
    }

    [HttpGet("{key}/attributes")]
    public async Task<ActionResult<IReadOnlyList<AttributeResponse>>> GetAttributes(string key, CancellationToken cancellationToken)
    {
        var result = await _catalogService.GetAttributesAsync(key, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(result.Error);
        }

        return Ok(result.Value);
    }    
}