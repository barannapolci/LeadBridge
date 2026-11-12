using CSharpFunctionalExtensions;

namespace LeadBridge.Core.Domain.Entities;

public sealed class Avatar
{
    public const int MaxLength = 2200;

    private Avatar()
    {
    }

    private Avatar(string avatarLink)
    {
        AvatarLink = avatarLink;
    }

    public string AvatarLink { get; private set; } = string.Empty;

    public static Result<Avatar> Create(string avatarLink)
    {
        if (string.IsNullOrWhiteSpace(avatarLink))
        {
            return Result.Failure<Avatar>("Avatar link cannot be empty.");
        }

        if (avatarLink.Length > MaxLength)
        {
            return Result.Failure<Avatar>($"Avatar link can not be longer than {MaxLength} characters.");
        }

        if (!Uri.TryCreate(avatarLink, UriKind.Absolute, out var uri)
            || (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            return Result.Failure<Avatar>("Avatar link must be a valid http or https address.");
        }

        return Result.Success(new Avatar(avatarLink.Trim()));
    }
}