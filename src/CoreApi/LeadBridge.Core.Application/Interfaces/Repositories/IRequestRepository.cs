using CSharpFunctionalExtensions;
namespace LeadBridge.Core.Application.Interfaces;
public interface IRequestRepository
{
    Task<Result> AddASync(CancellationToken cancellationToken);

    Task<Result> FindAllAsync(CancellationToken cancellationToken);

    Task<Result> UpdateAsync(CancellationToken cancellationToken);
}