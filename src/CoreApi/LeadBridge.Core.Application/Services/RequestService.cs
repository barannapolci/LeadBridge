using System.Collections;
using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Application.Interfaces.Services;
using LeadBridge.Core.Application.Mappings;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.Enums;
using LeadBridge.Core.Domain.ValueObjects;

namespace LeadBridge.Core.Application.Services;
public class RequestService : IRequestService
{
    private readonly IRequestRepository _repository;

    public RequestService(IRequestRepository requestRepository)
    {
        _repository = requestRepository;
    }

    public async Task<Result<Guid>> CreateAsync(Guid userId, RequestSide side, CreateRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var items = new List<RequestItem>();

        for (var index = 0; index < requestDto.Items.Count; index++)
        {
            var itemResult = BuildItem(requestDto.Items[index]);

            if (itemResult.IsFailure)
            {
                return Result.Failure<Guid>($"items[{index}]: {itemResult.Error}");
            }

            items.Add(itemResult.Value);
        }

        var requestResult = Request.Create(userId, side, items);

        if (requestResult.IsFailure)
        {
            return Result.Failure<Guid>(requestResult.Error);
        }

        var request = requestResult.Value;
        var addResult = await _repository.AddAsync(request, cancellationToken);

        if (addResult.IsFailure)
        {
            return Result.Failure<Guid>(addResult.Error);
        }

        return Result.Success(request.Id);
    }

    public async Task<IReadOnlyList<RequestResponse>> GetOwnRequestsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var requests = await _repository.GetByOwnerAsync(userId, cancellationToken);

        return requests.Select(r => r.ToResponse()).ToList();
    }

    public async Task<Result<RequestResponse>> GetOwnRequestAsync(Guid userId, Guid requestId, CancellationToken cancellationToken = default)
    {
        var requestMaybe = await _repository.GetByIdAsync(requestId, cancellationToken);

        if (requestMaybe.HasNoValue || requestMaybe.Value.OwnerId != userId)
        {
            return Result.Failure<RequestResponse>("request was not found");
        }

        return Result.Success(requestMaybe.Value.ToResponse());
    }

    private static Result<RequestItem> BuildItem(CreateRequestItemDto itemDto)
    {
        var conditions = new List<Condition>();

        foreach (var conditionDto in itemDto.Conditions)
        {
            if (IsEmpty(conditionDto))
            {
                continue;
            }

            var conditionResult = BuildCondition(conditionDto);

            if (conditionResult.IsFailure)
            {
                return Result.Failure<RequestItem>($"{conditionDto.AttributeKey}: {conditionResult.Error}");
            }

            conditions.Add(conditionResult.Value);
        }

        return RequestItem.Create(itemDto.CategoryKey, conditions);
    }

    private static Result<Condition> BuildCondition(ConditionDto conditionDto)
    {
        if (conditionDto.Type == AttributeType.Range)
        {
            return Condition.Range(conditionDto.AttributeKey, conditionDto.Min, conditionDto.Max);
        }

        if (conditionDto.Type == AttributeType.Set)
        {
            return Condition.Set(conditionDto.AttributeKey, conditionDto.Values ?? Array.Empty<string>());
        }

        return Result.Failure<Condition>("unknown attribute type");
    }

    private static bool IsEmpty(ConditionDto conditionDto)
    {
        if (conditionDto.Type == AttributeType.Range)
        {
            return conditionDto.Min is null && conditionDto.Max is null;
        }

        if (conditionDto.Type == AttributeType.Set)
        {
            return conditionDto.Values is null || conditionDto.Values.All(string.IsNullOrWhiteSpace);
        }

        return false;
    }
}