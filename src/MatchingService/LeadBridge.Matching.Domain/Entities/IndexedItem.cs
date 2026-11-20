using CSharpFunctionalExtensions;
using LeadBridge.Matching.Domain.Enums;
using LeadBridge.Matching.Domain.ValueObjects;

namespace LeadBridge.Matching.Domain.Entities;

public class IndexedItem
{
    public const int MaxLengthOfCategoryKey = 100;

    private readonly List<Condition> _conditions = new();

    private IndexedItem()
    {
    }

    private IndexedItem(Guid requestId, int requestVersion, RequestSide side, string categoryKey, List<Condition> conditions)
    {
        Id = Guid.NewGuid();
        RequestId = requestId;
        RequestVersion = requestVersion;
        Side = side;
        CategoryKey = categoryKey;
        _conditions = conditions;
    }

    public Guid Id { get; private set; }

    public Guid RequestId { get; private set; }

    public int RequestVersion { get; private set; }

    public RequestSide Side { get; private set; }

    public string CategoryKey { get; private set; } = string.Empty;

    public IReadOnlyList<Condition> Conditions => _conditions.AsReadOnly();

    public static Result<IndexedItem> Create(
        Guid requestId,
        int requestVersion,
        RequestSide side,
        string categoryKey,
        IEnumerable<Condition> conditions)
    {
        if (requestId == Guid.Empty)
        {
            return Result.Failure<IndexedItem>("request id can not be empty");
        }

        if (requestVersion < 1)
        {
            return Result.Failure<IndexedItem>("request version must be positive");
        }

        if (string.IsNullOrWhiteSpace(categoryKey) || categoryKey.Length > MaxLengthOfCategoryKey)
        {
            return Result.Failure<IndexedItem>("category key is empty or too long");
        }

        var list = conditions.ToList();

        if (list.Select(c => c.AttributeKey).Distinct().Count() != list.Count)
        {
            return Result.Failure<IndexedItem>("each attribute can be used only once per item");
        }

        return Result.Success(new IndexedItem(requestId, requestVersion, side, categoryKey.Trim(), list));
    }
}