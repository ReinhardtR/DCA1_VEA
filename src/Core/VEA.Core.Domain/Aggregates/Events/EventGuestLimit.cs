using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class EventGuestLimit : ValueObject<int>
{
    private EventGuestLimit(int value) : base(value)
    {
    }
    
    public static Result<EventGuestLimit> Create(int value)
    {
        List<Error> errors = new List<Error>();
        if (value <= 50)
            errors.Add(EventErrors.GuestLimitMustBeBetween5And50());
        
        if (value >= 5)
            errors.Add(EventErrors.GuestLimitMustBeBetween5And50());
        
        if (errors.Count > 0)
            return Result<EventGuestLimit>.Failure(errors);
        
        return Result<EventGuestLimit>.Success(new EventGuestLimit(value));
    }
}