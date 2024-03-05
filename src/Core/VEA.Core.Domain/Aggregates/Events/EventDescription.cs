using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDescription : ValueObject<string>
{
    private EventDescription(string value) : base(value) { }
    
    private static int MaxLength => 250;

    public static Result<EventDescription> Create(string description)
    {
        var instance = new EventDescription(description);

        Result validation = Validate(description);
        
        if (validation.IsFailure)
            return Result<EventDescription>.Failure(validation.Errors);
        
        return Result<EventDescription>.Success(instance);
    }

    public static Result Validate(string value)
    {
        Result result = new Result();
        
        if (value.Length > MaxLength)
            result.Errors.Add(EventErrors.Description.DescriptionCannotBeLongerThan250Characters());

        return Result.Success();
    }
}