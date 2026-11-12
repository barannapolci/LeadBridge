namespace LeadBridge.Core.Domain.Entities;

public class Category
{
    public const int MaxLengthOfCategoryName = 250;
    private readonly List<AttributeDefinition> _attributeDefinitions = new();

    private Category()
    {
    }

    private Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }
    
    public IReadOnlyList<AttributeDefinition> Attributes => _attributeDefinitions.AsReadOnly();
    public string Name { get; private set; } = string.Empty;
}