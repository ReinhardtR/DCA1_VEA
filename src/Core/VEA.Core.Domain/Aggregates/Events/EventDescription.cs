using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDescription : ValueObject<string>
{
    private EventDescription(string value) : base(value) { }
    
    private static int MaxLength => 250;

    public static Result<EventDescription> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            value = "";
        
        var validation = Result.Validator()
            .Assert(value.Length < MaxLength, Errors.DescriptionCannotBeLongerThan250Characters())
            .Validate();
        
        return validation.IsFailure
            ? Result.Failure<EventDescription>(validation.Errors)
            : Result.Success(new EventDescription(value));
    }
    
    

    public class Errors
    {
        public static Error DescriptionCannotBeLongerThan250Characters() =>
                new(ErrorType.InvalidArgument, 3, "Description cannot be longer than 250 characters");
            public static Error DescriptionCannotBeModifiedForCanceledEvent() =>
                new(ErrorType.InvalidOperation, 4, "Description cannot be modified for canceled event");
            public static Error DescriptionCannotBeEmpty() =>
                new(ErrorType.InvalidArgument, 5, "Description cannot be empty");
        
            public static Error CannotUpdateCancelledEvent() =>
                new(ErrorType.InvalidOperation, 24, "Cannot update cancelled event");
        
            public static Error CannotUpdateActiveEvent() => 
                new(ErrorType.InvalidOperation, 25, "Cannot update started event");
    }
}