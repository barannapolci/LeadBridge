namespace LeadBridge.Core.Application.DTOs.Account
{
    public sealed record class AuthResponse(string AccessToken, DateTime ExpiresAt);
}
