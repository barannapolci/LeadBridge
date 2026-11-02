using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Account;

namespace LeadBridge.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);

        Task<Result<AuthResponse>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default);
    }
}
