using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Profile;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Interfaces.Repositories
{
    public interface IProfileRepository
    {
        Task UpdateAsync(CancellationToken cancellationToken);
        Task<Maybe<PersonalInfo>> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
