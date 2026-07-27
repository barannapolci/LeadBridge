using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Components.RenderTree;

namespace LeadBridge.Core.Domain.Entities;

public class PersonalInfo
{
    private readonly List<BusinessType> _businessTypes = new();

    private PersonalInfo()
    {
    }

    private PersonalInfo(Guid id, string firstName, string lastName, int year, string country, string region, string city)
    {
        Id = id;
        Country = country;
        Region = region;
        City = city;
        FirstName = firstName;
        LastName = lastName;
        Year = year;
    }

    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public int Year { get; private set; }
    public string? Country { get; private set; }
    public string? Region { get; private set; }
    public string? City { get; private set; }

    public IReadOnlyCollection<BusinessType> BusinessTypes => _businessTypes.AsReadOnly();

    public static Result<PersonalInfo> Create(string firstName, string lastName, int year, string country, string region, string city)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
        {
            return Result.Failure<PersonalInfo>("first or last name can not be empty");
        }

        if (year < 1950 || year > DateTime.UtcNow.Year)
        {
            return Result.Failure<PersonalInfo>($"year can not be  less than 1950 or  greater than {DateTime.UtcNow.Year}");
        }

        var personalInfo = new PersonalInfo(Guid.NewGuid(), firstName, lastName, year, country, region, city);

        return Result.Success(personalInfo);
    }

    public Result AddBusinessType(BusinessType businessType)
    {
        if (_businessTypes.Contains(businessType))
        {
            return Result.Failure("this business is already in use");
        }

        _businessTypes.Add(businessType);
        return Result.Success();
    }

    public Result DeleteBusinessType(BusinessType businessType)
    {
        if (!_businessTypes.Contains(businessType))
        {
            return Result.Failure("this business is already absent");
        }

        _businessTypes.Remove(businessType);
        return Result.Success();
    }
}