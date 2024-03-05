using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventId : ValueObject<Guid>
{
    public EventId(Guid value) : base(value) { }

    public static Result<EventId> New()
    {
        var instance = new EventId(Guid.NewGuid());
        return Result<EventId>.Success(instance);
    }
}