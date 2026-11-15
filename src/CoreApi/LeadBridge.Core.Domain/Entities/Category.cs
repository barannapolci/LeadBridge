namespace LeadBridge.Core.Domain.Entities;

public class Category
{
    public const int MaxLengthOfCategoryName = 250;
    public const int MaxLengthOfCategoryKey = 100;

    private readonly List<AttributeDefinition> _attributes = new();

    private Category()
    {
    }

    public Guid Id { get; private set; }

    public string Key { get; private set; } = string.Empty;

    public string Name { get; private set; } = string.Empty;

    public IReadOnlyList<AttributeDefinition> Attributes => _attributes.AsReadOnly();
}