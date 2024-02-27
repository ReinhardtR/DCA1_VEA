using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class EventDescription : ValueObject<string>
{

    private EventDescription(string value) : base(value) 
        => Value = value;
    
    private static int MaxLength => 250;

    public static Result<EventDescription> Create(string description)
    {
        var instance = new EventDescription(description);

        Result validation = instance.Validate(description);
        if (validation.IsFailure)
            return Result<EventDescription>.Failure(validation.Errors);
        
        return Result<EventDescription>.Success(instance);
    }


    public Result Validate(string value)
    {
        Result result = new Result();
        
        if (string.IsNullOrWhiteSpace(value))
            result.Errors.Add(EventErrors.DescriptionCannotBeEmpty());

        if (value.Length > MaxLength)
            result.Errors.Add(EventErrors.DescriptionCannotBeLongerThan250Characters());

        return Result.Success();
    }
    
    
}