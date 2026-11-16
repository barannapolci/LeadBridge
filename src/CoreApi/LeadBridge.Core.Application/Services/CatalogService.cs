using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeadBridge.Core.Application.Services;
public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task<IReadOnlyList<CategoryResponse>> GetAsync(CancellationToken cancellationToken = default)
    {
        var res = await _catalogRepository.GetCategoriesAsync(cancellationToken);
        var response = res.Select(r => new CategoryResponse(r.Key, r.Name)).ToList();

        return response;
    }
}