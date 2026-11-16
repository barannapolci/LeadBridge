using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Request;

namespace LeadBridge.Core.Application.Interfaces.Services;
public interface ICatalogService
{
    Task<IReadOnlyList<CategoryResponse>> GetAsync(CancellationToken cancellationToken);
}