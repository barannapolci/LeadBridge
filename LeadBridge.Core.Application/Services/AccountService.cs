using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Account;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Application.Interfaces.Security;
using LeadBridge.Core.Application.Interfaces.Services;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AccountService(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            var personalInfoResult = PersonalInfo.Create(
                request.FirstName,
                request.LastName,
                request.Year,
                request.Country,
                request.Region,
                request.City);

            if (personalInfoResult.IsFailure)
            {
                return Result.Failure(personalInfoResult.Error);
            }

            var personalInfo = personalInfoResult.Value;

            string email = request.Email.Trim().ToLowerInvariant();

            var emailTaken = await _userRepository.ExistsByEmailAsync(email, cancellationToken);

            if (emailTaken)
            {
                return Result.Failure("Email is already registered.");
            }

            var userResult = User.Create(
                email,
                _passwordHasher.Hash(request.Password),
                request.Role,
                personalInfo);

            if (userResult.IsFailure)
            {
                return Result.Failure(userResult.Error);
            }

            var user = userResult.Value;

            return await _userRepository.AddAsync(user, cancellationToken);
        }

        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest loginRequest, CancellationToken cancellationToken = default)
        {
            string email = loginRequest.Email.Trim().ToLowerInvariant();

            var userResult = await _userRepository.FindByEmailAsync(email, cancellationToken);

            if (userResult.HasNoValue)
            {
                return Result.Failure<AuthResponse>("Invalid email or password.");
            }

            var user = userResult.Value;

            if (!_passwordHasher.Verify(loginRequest.Password, user.PasswordHash))
            {
                return Result.Failure<AuthResponse>("Invalid email or password.");
            }

            if (!user.IsEmailConfirmed)
            {
                return Result.Failure<AuthResponse>("confirm email to login in");
            }

            var token = _jwtTokenGenerator.Generate(user);

            return Result.Success(new AuthResponse(token.Value, token.ExpiresAtUtc));
        }
    }
}
