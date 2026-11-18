using System.Collections;
using CSharpFunctionalExtensions;
using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Application.Interfaces;
using LeadBridge.Core.Application.Interfaces.Services;
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

    public async Task<Result> Create(Guid userId, RequestSide side, CreateRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var conditions = new List<Condition>();
        var requestItems = new List<RequestItem>();
        foreach (var i in requestDto.Items)
        {
            foreach (var c in i.Conditions)
            {
                Result<Condition> condition = c.Type switch
                {
                    AttributeType.Set when c.Values is not null && c.Values.Count > 0 => Condition.Set(c.AttributeKey, c.Values),
                    AttributeType.Set => Result.Failure<Condition>("set values are required"),
                    AttributeType.Range => Condition.Range(c.AttributeKey, c.Min, c.Max),
                    _ => Result.Failure<Condition>("unknown attribute type"),

                };

                if (condition.IsFailure)
                {
                    return Result.Failure($"item:{i},categoryKey: {i.CategoryKey} , error{condition.Error} ");
                }

                conditions.Add(condition.Value);
            }

            var requestItem = RequestItem.Create(i.CategoryKey, conditions);
            
            if (requestItem.IsFailure)
            {
                return Result.Failure(requestItem.Error);    
            }

            requestItems.Add(requestItem.Value);
            conditions.Clear();
        }

        var requestResult = Request.Create(userId, side, requestItems);
        
        if (requestResult.IsFailure)
        {
            return Result.Failure($"can not create Request, error: {requestResult.Error}");
        }

        return await _repository.AddAsync(requestResult.Value, cancellationToken);
    }
}