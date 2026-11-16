using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Entities;
namespace LeadBridge.Core.Application.Interfaces;

public interface ICatalogRepository
{
    Task<IReadOnlyList<Category>> GetCategoriesAsync(CancellationToken cancellationToken = default);
}