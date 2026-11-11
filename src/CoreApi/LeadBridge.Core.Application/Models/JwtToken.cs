namespace LeadBridge.Core.Application.Models
{
    public sealed class JwtToken
    {
        public JwtToken(string value, DateTime expiresAtUtc)
        {
            Value = value;
            ExpiresAtUtc = expiresAtUtc;
        }

        public string Value { get; init; }

        public DateTime ExpiresAtUtc { get; init; }
    }
}
