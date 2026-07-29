namespace LeadBridge.Core.Domain.Entities;
using CSharpFunctionalExtensions;

/// <summary>
/// User entity with information for register and login.
/// </summary>
public class User
{
    private readonly List<Reviews> _receivedReviews = new();
    private readonly List<Reviews> _givenReviews = new();
    private User()
    {
    }

    private User(Guid id, string email, string passwordHash, UserRole userRole)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        UserRole = userRole;
        IsEmailConfirmed = false;
        RegisterDate = DateTime.UtcNow;
        AvgReviews = 0;
    }

    public Guid Id { get; private set; }
    public string Email { get; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsEmailConfirmed { get; private set; }
    public Guid PersonalInfoId { get; private set; }
    public PersonalInfo? PersonalInfo { get; private set; }
    public UserRole UserRole { get;  private set; }
    public DateTime RegisterDate { get;  private set; }
    public double AvgReviews { get;  private set; }
    public Guid? AvatarId { get; private set; }
    public Avatar? Avatar { get; private set; }

    public IReadOnlyCollection<Reviews> ReceivedReviews => _receivedReviews.AsReadOnly();
    public IReadOnlyCollection<Reviews> GivenReviews => _givenReviews.AsReadOnly();

    public static Result<User> Create(string email, string passwordHash, UserRole userRole)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(passwordHash))
        {
            return Result.Failure<User>("email or password is empty");
        }

        if (!email.Contains("@gmail.com", StringComparison.OrdinalIgnoreCase))
        {
            return Result.Failure<User>("email has not correct format");
        }

        User user = new User(Guid.NewGuid(), email, passwordHash, userRole);
        return Result.Success<User>(user);
    }

    public void SetAvatar(Guid avatarId)
    {
        AvatarId = avatarId;
    }

    public void SetPersonalInfo(Guid personalInfoId)
    {
        PersonalInfoId = personalInfoId;
    }
}
