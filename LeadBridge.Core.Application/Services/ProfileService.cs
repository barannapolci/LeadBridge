using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.BusinessType;
using LeadBridge.Core.Application.DTOs.Profile;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Application.Interfaces.Services;

namespace LeadBridge.Core.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<Result> ChangeAsync(Guid userId, UpdateProfileRequest updateProfileRequest, CancellationToken cancellationToken)
        {
            var res = await _profileRepository.FindByUserIdAsync(userId, cancellationToken);

            if (res.HasNoValue)
            {
                return Result.Failure("users profile was not found");
            }

            var personalInfo = res.Value;

            var resInfo = personalInfo.Update(updateProfileRequest.FirstName, updateProfileRequest.LastName, updateProfileRequest.Year, updateProfileRequest.Country, updateProfileRequest.Region, updateProfileRequest.City);
            await _profileRepository.UpdateAsync(cancellationToken);
            
            return resInfo;
        }

        public async Task<Result<ProfileResponse>> GetAsync(Guid userId, CancellationToken cancellationToken)
        {
            var res = await _profileRepository.FindByUserIdAsync(userId, cancellationToken);            
            
            if (res.HasNoValue)
            {
                return Result.Failure<ProfileResponse>("users profile was not found");
            }

            var personalInfo = res.Value;

            var businessTypes = personalInfo.BusinessTypes
                                .Select(b => new BusinessTypeResponse(b.Id, b.Name))
                                .ToList();

            var response = new ProfileResponse(
                personalInfo.FirstName,
                personalInfo.LastName,
                personalInfo.Year,
                personalInfo.Country,
                personalInfo.Region,
                personalInfo.City,
                businessTypes);

            return Result.Success(response);
        }
    }    
}