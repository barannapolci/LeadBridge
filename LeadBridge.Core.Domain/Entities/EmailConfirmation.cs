namespace LeadBridge.Core.Domain.Entities;
using CSharpFunctionalExtensions;

public class EmailConfirmation
{
    private EmailConfirmation()
    {
    }

    private EmailConfirmation(string confirmationToken, Guid userId)
    {
        Id = Guid.NewGuid();
        ConfirmationToken = confirmationToken;
        UserId = userId;
        CreateDate = DateTime.UtcNow;
        ExpireDate = DateTime.UtcNow.AddHours(24);
    }

    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public User? User { get; private set; }

    public string ConfirmationToken { get; private set; } = string.Empty;

    public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

    public DateTime ExpireDate { get; private set; } = DateTime.UtcNow;

    public static Result<EmailConfirmation> Create(string confirmationToken, Guid userId)
    {
        if (userId == Guid.Empty)
        {
            return Result.Failure<EmailConfirmation>("User ID cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(confirmationToken))
        {
            return Result.Failure<EmailConfirmation>("Confirmation token cannot be empty.");
        }

        var confirmation = new EmailConfirmation(confirmationToken, userId);

        return Result.Success(confirmation);
    }

    public bool IsExpired()
    {
        return ExpireDate < DateTime.UtcNow;
    }
}