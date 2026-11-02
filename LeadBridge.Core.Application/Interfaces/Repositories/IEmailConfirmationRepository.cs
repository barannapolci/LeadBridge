using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Entities;

namespace LeadBridge.Core.Application.Interfaces.Repositories
{
    public interface IEmailConfirmationRepository
    {
        Task AddAsync(EmailConfirmation confirmation, CancellationToken cancellationToken = default);

        Task<Maybe<EmailConfirmation>> FindByTokenAsync(string token, CancellationToken cancellationToken = default);

        Task DeleteAsync(EmailConfirmation confirmation, CancellationToken cancellationToken = default);
    }
}