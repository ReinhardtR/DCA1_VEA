using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventGuestLimit : ValueObject<int>
{
    private EventGuestLimit(int value) : base(value) { }
    
    public static Result<EventGuestLimit> Create(int value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<EventGuestLimit>.Failure(validation.Errors)
            : Result<EventGuestLimit>.Success(new EventGuestLimit(value));
    }
    
    public static Result Validate(int value)
    {
        List<Error> errors = [];
        
        if (value > 50)
            errors.Add(EventErrors.GuestLimit.GuestLimitMustBeBetween5And50());
        
        if (value < 5)
            errors.Add(EventErrors.GuestLimit.GuestLimitMustBeBetween5And50());
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}