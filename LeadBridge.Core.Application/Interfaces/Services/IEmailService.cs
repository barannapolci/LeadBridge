namespace LeadBridge.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string token, CancellationToken cancellationToken = default);
    }
}