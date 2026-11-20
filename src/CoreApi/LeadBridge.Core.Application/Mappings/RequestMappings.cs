using LeadBridge.Core.Application.DTOs.Request;
using LeadBridge.Core.Domain.Entities;
using LeadBridge.Core.Domain.ValueObjects;

namespace LeadBridge.Core.Application.Mappings;

public static class RequestMappings
{
    public static RequestResponse ToResponse(this Request request)
    {
        var items = request.Items.Select(ToItemResponse).ToList();

        return new RequestResponse(
            request.Id,
            request.Side,
            request.Status,
            request.Version,
            request.CreatedAt,
            items);
    }

    private static RequestItemResponse ToItemResponse(RequestItem item)
    {
        var conditions = item.Conditions.Select(ToConditionResponse).ToList();

        return new RequestItemResponse(item.CategoryKey, conditions);
    }

    private static ConditionResponse ToConditionResponse(Condition condition)
    {
        return new ConditionResponse(
            condition.AttributeKey,
            condition.Type,
            condition.Min,
            condition.Max,
            condition.Values);
    }
}