using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Core.Infrastructure.Postgres.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Maybe<User>> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            return Maybe.From(user);
        }

        public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.Email == email, cancellationToken);
        }
    }
}
