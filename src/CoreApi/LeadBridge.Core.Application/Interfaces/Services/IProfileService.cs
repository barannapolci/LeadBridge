using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Profile;

namespace LeadBridge.Core.Application.Interfaces.Services
{
    public interface IProfileService
    {
        Task<Result> ChangeAsync(Guid userId, UpdateProfileRequest updateProfileRequest, CancellationToken cancellationToken = default);

        Task<Result<ProfileResponse>> GetAsync(Guid userId, CancellationToken cancellationToken = default);
    }   
}
