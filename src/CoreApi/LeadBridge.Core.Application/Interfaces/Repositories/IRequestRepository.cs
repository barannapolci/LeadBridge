using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Entities;
namespace LeadBridge.Core.Application.Interfaces;
public interface IRequestRepository
{
    Task<Result> AddAsync(Request request, CancellationToken cancellationToken = default);
}