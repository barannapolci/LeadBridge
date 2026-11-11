using LeadBridge.Core.Application.DTOs.BusinessType;

namespace LeadBridge.Core.Application.DTOs.Profile
{
    public sealed record class ProfileResponse(string FirstName, string LastName, int Year, string Country, string Region, string City, IReadOnlyList<BusinessTypeResponse> BusinessTypes);
}
