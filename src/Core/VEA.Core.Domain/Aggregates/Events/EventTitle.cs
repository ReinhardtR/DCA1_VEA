using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventTitle : ValueObject<string>
{
    private EventTitle(string value) : base(value) { }

    private const int MinLength = 3;
    private const int MaxLength = 75;

    public static Result<EventTitle> Create(string value)
    {
        var validation = Validate(value);
        return validation.IsFailure
            ? Result.Failure<EventTitle>(validation.Errors)
            : Result.Success(new EventTitle(value!));
    }

    public static Result Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Validator()
                .Assert(!string.IsNullOrWhiteSpace(value), Errors.TitleMustBeBetween3And75Characters())
                .Validate();
        
        return Result.Validator()
            .Assert(value.Length is > MinLength and < MaxLength, Errors.TitleMustBeBetween3And75Characters())
            .Validate();
    }

    public class Errors
    {
        public static Error TitleMustBeBetween3And75Characters() =>
            new(ErrorType.InvalidArgument, 6, "Title must be between 3 and 75 characters");
        public static Error UpdateTitleWhenEventActive() =>
            new(ErrorType.InvalidArgument, 7, "Cannot update title when event is active");
        public static Error UpdateTitleWhenEventCancelled() =>
            new(ErrorType.InvalidArgument, 8, "Cannot update title when event is cancelled");
    }
    
   //overide Object.equals
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((EventTitle) obj);
    }
}