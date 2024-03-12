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
            ? Result.Failure<EventGuestLimit>(validation.Errors)
            : Result.Success(new EventGuestLimit(value));
    }

    public static Result Validate(int value)
    {
        return Result.Validator()
            .Assert(value <= 50, Errors.GuestLimitMustBeBetween5And50())
            .Assert(value >= 5, Errors.GuestLimitMustBeBetween5And50())
            .Validate();
    }

    public static class Errors
    {
        public static Error GuestLimitMustBeBetween5And50() =>
            new(ErrorType.InvalidArgument, 9, "Guest limit must be between 5 and 50");
        
        public static Error CannotUpdateGuestLimitWhenEventActive() =>
            new(ErrorType.InvalidArgument, 10, "Cannot update guest limit when event is active");
        public static Error CannotUpdateGuestLimitWhenEventCancelled() =>
            new(ErrorType.InvalidArgument, 10, "Cannot update guest limit when event is cancelled");
    }
}