using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace LeadBridge.Core.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool TryGetUserId(this ClaimsPrincipal principal, out Guid userId)
        {
            return Guid.TryParse(principal.FindFirstValue(JwtRegisteredClaimNames.Sub), out userId);
        }
    }
}