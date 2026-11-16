using System.ComponentModel;
using System.Net.Http.Headers;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;

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
    public async Task<ActionResult<IReadOnlyList<CategoryResponse>>> Get(CancellationToken cancellationToken = default)
    {
        return Ok(await _catalogService.GetAsync(cancellationToken));
    }
}