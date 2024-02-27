using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class EventId : ValueObject<Guid>
{
    public EventId(Guid value) : base(value) 
        => Value = value;

    public static Result<EventId> New()
    {
        var instance = new EventId(Guid.NewGuid());
        return Result<EventId>.Success(instance);
    }
}