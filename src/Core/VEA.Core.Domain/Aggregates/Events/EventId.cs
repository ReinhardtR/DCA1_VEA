using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class EventId : ValueObject<Guid>
{
    private EventId(Guid value) : base(value)
    {
        
    }
    
    public static Result<EventId> Create(Guid id)
    {
        return Result<EventId>.Success(new EventId(id));
    }
    
    
}