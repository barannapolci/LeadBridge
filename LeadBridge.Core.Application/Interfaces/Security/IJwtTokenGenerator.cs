using LeadBridge.Core.Application.Models;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Interfaces.Security
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user);
    }
}
