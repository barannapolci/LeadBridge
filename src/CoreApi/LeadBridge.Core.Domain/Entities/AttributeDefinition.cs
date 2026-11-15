using LeadBridge.Core.Domain.Enums;

namespace LeadBridge.Core.Domain.Entities;

public class AttributeDefinition
{
    public const int MaxLengthOfKey = 100;
    public const int MaxLengthOfName = 250;
    public const int MaxLengthOfUnit = 50;

    private AttributeDefinition()
    {
    }

    public Guid Id { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Key { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public AttributeType Type { get; private set; }

    public string? Unit { get; private set; }

    public IReadOnlyList<string> AllowedValues { get; private set; } = Array.Empty<string>();
}