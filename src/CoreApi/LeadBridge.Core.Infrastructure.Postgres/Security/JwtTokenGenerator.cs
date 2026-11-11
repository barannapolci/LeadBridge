using System.Security.Claims;
using System.Text;
using LeadBridge.Core.Application.Interfaces.Security;
using LeadBridge.Core.Application.Models;
using LeadBridge.Core.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace LeadBridge.Core.Infrastructure.Postgres.Security
{
    public sealed class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _settings;
        private readonly SigningCredentials _signingCredentials;
        private readonly JsonWebTokenHandler _handler = new();

        public JwtTokenGenerator(IOptions<JwtSettings> options)
        {
            _settings = options.Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            _signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        public JwtToken Generate(User user)
        {
            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_settings.AccessTokenLifetimeMinutes);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("role", user.UserRole.ToString()),
                }),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                IssuedAt = now,
                NotBefore = now,
                Expires = expires,
                SigningCredentials = _signingCredentials,
            };

            var token = _handler.CreateToken(descriptor);

            return new JwtToken(token, expires);
        }
    }
}