using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Domain.Entities;
public class Request
{
    private readonly List<Category> _categories = new();
    private Request()
    {
    }

    private Request(Guid id, Guid ownerId, RequestSide side, Status status, int version)
    {
        Id = id;
        OwnerId = ownerId;
        Side = side;
        RequestStatus = status;
        Version = version;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public RequestSide Side { get; private set; }
    public Status RequestStatus { get; private set; }
    public IReadOnlyList<Category> Categorys => _categories.AsReadOnly();
    public int Version { get; private set; }
    public DateTime CreatedAt { get; private set; }
}
