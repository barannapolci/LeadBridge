using CSharpFunctionalExtensions;

namespace LeadBridge.Core.Domain.ValueObjects;

public sealed class RequestItem
{
    public const int MaxConditions = 30;

    private readonly List<Condition> _conditions = new();

    private RequestItem()
    {
    }

    private RequestItem(string categoryKey, List<Condition> conditions)
    {
        CategoryKey = categoryKey;
        _conditions = conditions;
    }

    public string CategoryKey { get; private set; } = string.Empty;

    public IReadOnlyList<Condition> Conditions => _conditions.AsReadOnly();

    public static Result<RequestItem> Create(string categoryKey, IEnumerable<Condition> conditions)
    {
        if (string.IsNullOrWhiteSpace(categoryKey))
        {
            return Result.Failure<RequestItem>("category key can not be empty");
        }

        var list = conditions.ToList();

        if (list.Count > MaxConditions)
        {
            return Result.Failure<RequestItem>($"item can not contain more than {MaxConditions} conditions");
        }

        if (list.Select(c => c.AttributeKey).Distinct().Count() != list.Count)
        {
            return Result.Failure<RequestItem>("each attribute can be used only once per item");
        }

        return Result.Success(new RequestItem(categoryKey.Trim(), list));
    }
}