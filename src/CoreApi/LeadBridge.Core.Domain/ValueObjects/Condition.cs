using CSharpFunctionalExtensions;
using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Domain.ValueObjects;

public sealed class Condition
{
    private Condition()
    {
    }

    private Condition(string attributeKey, AttributeType type, decimal? min, decimal? max, IReadOnlyList<string> values)
    {
        AttributeKey = attributeKey;
        Type = type;
        Min = min;
        Max = max;
        Values = values;
    }

    public string AttributeKey { get; private set; } = string.Empty;

    public AttributeType Type { get; private set; }

    public decimal? Min { get; private set; }

    public decimal? Max { get; private set; }

    public IReadOnlyList<string> Values { get; private set; } = Array.Empty<string>();

    public static Result<Condition> Range(string attributeKey, decimal? min, decimal? max)
    {
        if (string.IsNullOrWhiteSpace(attributeKey))
        {
            return Result.Failure<Condition>("attribute key can not be empty");
        }

        if (min is null && max is null)
        {
            return Result.Failure<Condition>("range must have at least one bound");
        }

        if (min is not null && max is not null && min > max)
        {
            return Result.Failure<Condition>("range minimum can not be greater than maximum");
        }

        return Result.Success(new Condition(attributeKey.Trim(), AttributeType.Range, min, max, Array.Empty<string>()));
    }

    public static Result<Condition> Set(string attributeKey, IEnumerable<string> values)
    {
        if (string.IsNullOrWhiteSpace(attributeKey))
        {
            return Result.Failure<Condition>("attribute key can not be empty");
        }

        var cleaned = values
            .Select(v => v.Trim())
            .Where(v => v.Length > 0)
            .Distinct()
            .ToList();

        if (cleaned.Count == 0)
        {
            return Result.Failure<Condition>("set must contain at least one value");
        }

        return Result.Success(new Condition(attributeKey.Trim(), AttributeType.Set, null, null, cleaned));
    }
}