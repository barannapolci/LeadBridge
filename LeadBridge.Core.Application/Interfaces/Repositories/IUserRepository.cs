using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<Result> AddAsync(User user, CancellationToken cancellationToken = default);

        Task<Maybe<User>> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    }
}