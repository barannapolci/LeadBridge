using CSharpFunctionalExtensions;

namespace LeadBridge.Core.Domain.Entities;

public class Reviews
{
    private Reviews()
    {
    }

    private Reviews(Guid senderId, Guid recipientId, int rating)
    {
        Id = Guid.NewGuid();
        SenderId = senderId;
        RecipientId = recipientId;
        Rating = rating;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }

    public Guid SenderId { get; private set;  }
    public User? Sender { get; private set; }

    public Guid RecipientId { get; private set; }
    public User? Recipient { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public int Rating { get; private set; }

    public static Result<Reviews> Create(Guid senderId, Guid recipientId, int rating)
    {
        if (senderId == Guid.Empty || recipientId == Guid.Empty)
        {
            return Result.Failure<Reviews>("id cannot be empty");
        }

        if (rating < 1 || rating > 5)
        {
            return Result.Failure<Reviews>("Rating must be between 1 and 5");
        }

        Reviews reviews = new Reviews(senderId, recipientId, rating);
        return Result.Success(reviews);
    }
}
