namespace LeadBridge.Core.Domain.Entities;
using CSharpFunctionalExtensions;

public class BusinessType
{
    private readonly List<PersonalInfo> _personalInfos = new();
    private BusinessType()
    {
    }

    private BusinessType(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<PersonalInfo> PersonalInfos => _personalInfos.AsReadOnly();
    public static Result<BusinessType> Create(string name, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<BusinessType>("Business type name cannot be empty.");
        }

        var businessType = new BusinessType(Guid.NewGuid(), name, description);
        return Result.Success(businessType);
    }
}