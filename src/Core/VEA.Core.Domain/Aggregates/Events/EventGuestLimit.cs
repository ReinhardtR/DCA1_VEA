using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventGuestLimit : ValueObject<int>
{
    private EventGuestLimit(int value) : base(value) { }
    
    public static Result<EventGuestLimit> Create(int value)
    {
        var validation = Result.Validator()
            .Assert(value > 50, EventErrors.GuestLimit.GuestLimitMustBeBetween5And50())
            .Assert(value < 5, EventErrors.GuestLimit.GuestLimitMustBeBetween5And50())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<EventGuestLimit>(validation.Errors)
            : Result.Success(new EventGuestLimit(value));
    }
}