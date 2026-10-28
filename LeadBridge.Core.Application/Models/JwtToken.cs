namespace LeadBridge.Core.Application.Models
{
    public sealed class JwtToken
    {
        public JwtToken(string value, DateTime expiresAtUtc)
        {
            Value = value;
            expiresAtUtc = ExpiresAtUtc;
        }

        public string Value { get; init; }

        public DateTime ExpiresAtUtc { get; init; }
    }
}
