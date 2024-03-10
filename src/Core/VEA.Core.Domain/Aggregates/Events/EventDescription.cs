using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDescription : ValueObject<string>
{
    private EventDescription(string value) : base(value) { }
    
    private static int MaxLength => 250;

    public static Result<EventDescription> Create(string value)
    {
        var validation = Result.Validator()
            .Assert(value.Length > MaxLength, EventErrors.Description.DescriptionCannotBeLongerThan250Characters())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<EventDescription>(validation.Errors)
            : Result.Success(new EventDescription(value));
    }
}