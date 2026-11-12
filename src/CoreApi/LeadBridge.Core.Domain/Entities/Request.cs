using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Enums;
using LeadBridge.Core.Domain.ValueObjects;

namespace LeadBridge.Core.Domain.Entities;

public class Request
{
    public const int MaxItems = 20;

    private readonly List<RequestItem> _items = new();

    private Request()
    {
    }

    private Request(Guid id, Guid ownerId, RequestSide side, List<RequestItem> items)
    {
        Id = id;
        OwnerId = ownerId;
        Side = side;
        Status = RequestStatus.Draft;
        Version = 1;
        CreatedAt = DateTime.UtcNow;
        _items = items;
    }

    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public RequestSide Side { get; private set; }

    public RequestStatus Status { get; private set; }

    public int Version { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public IReadOnlyList<RequestItem> Items => _items.AsReadOnly();

    public static Result<Request> Create(Guid ownerId, RequestSide side, IEnumerable<RequestItem> items)
    {
        if (ownerId == Guid.Empty)
        {
            return Result.Failure<Request>("owner id can not be empty");
        }

        var itemsResult = ValidateItems(items);

        if (itemsResult.IsFailure)
        {
            return Result.Failure<Request>(itemsResult.Error);
        }

        return Result.Success(new Request(Guid.NewGuid(), ownerId, side, itemsResult.Value));
    }

    public Result Activate()
    {
        if (Status != RequestStatus.Draft)
        {
            return Result.Failure("only a draft request can be activated");
        }

        Status = RequestStatus.Active;
        Version++;

        return Result.Success();
    }

    public Result Close()
    {
        if (Status == RequestStatus.Closed)
        {
            return Result.Failure("request is already closed");
        }

        Status = RequestStatus.Closed;
        Version++;

        return Result.Success();
    }

    public Result ReplaceItems(IEnumerable<RequestItem> items)
    {
        if (Status == RequestStatus.Closed)
        {
            return Result.Failure("closed request can not be changed");
        }

        var itemsResult = ValidateItems(items);

        if (itemsResult.IsFailure)
        {
            return Result.Failure(itemsResult.Error);
        }

        _items.Clear();
        _items.AddRange(itemsResult.Value);
        Version++;

        return Result.Success();
    }

    private static Result<List<RequestItem>> ValidateItems(IEnumerable<RequestItem> items)
    {
        var list = items.ToList();

        if (list.Count == 0)
        {
            return Result.Failure<List<RequestItem>>("request must contain at least one item");
        }

        if (list.Count > MaxItems)
        {
            return Result.Failure<List<RequestItem>>($"request can not contain more than {MaxItems} items");
        }

        return Result.Success(list);
    }
}