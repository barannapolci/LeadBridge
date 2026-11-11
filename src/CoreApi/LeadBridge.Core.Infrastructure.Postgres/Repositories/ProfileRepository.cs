using System.Globalization;
using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Profile;
using LeadBridge.Core.Application.Interfaces.Repositories;
using LeadBridge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeadBridge.Core.Infrastructure.Postgres.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Maybe<PersonalInfo>> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var res = await _context.Users.Include(u => u.PersonalInfo)
            .ThenInclude(p => p!.BusinessTypes)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            return Maybe.From(res?.PersonalInfo);
        }
    } 
}
