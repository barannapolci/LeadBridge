using CSharpFunctionalExtensions;

namespace LeadBridge.Core.Domain.Entities;

public class Avatar
{
    private Avatar()
    {
    }

    private Avatar(Guid id, string avatarLink)
    {
        Id = id;
        AvatarLink = avatarLink;
    }

    public Guid Id { get; private set; }
    public string AvatarLink { get; private set; } = string.Empty;

    public static Result<Avatar> Create(string avatarLink)
    {
        if (string.IsNullOrWhiteSpace(avatarLink))
        {
            return Result.Failure<Avatar>("Avatar link cannot be empty.");
        }

        var avatar = new Avatar(Guid.NewGuid(), avatarLink);
        return Result.Success(avatar);
    }
}