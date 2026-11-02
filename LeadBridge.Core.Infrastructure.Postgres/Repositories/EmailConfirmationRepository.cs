using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Core.Infrastructure.Postgres.Repositories
{
    public class EmailConfirmationRepository : IEmailConfirmationRepository
    {
        private readonly ApplicationDbContext _context;

        public EmailConfirmationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EmailConfirmation confirmation, CancellationToken cancellationToken = default)
        {
            _context.EmailConfirmation.Add(confirmation);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Maybe<EmailConfirmation>> FindByTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            var confirmation = await _context.EmailConfirmation
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.ConfirmationToken == token, cancellationToken);

            return Maybe.From(confirmation);
        }

        public async Task DeleteAsync(EmailConfirmation confirmation, CancellationToken cancellationToken = default)
        {
            _context.EmailConfirmation.Remove(confirmation);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}