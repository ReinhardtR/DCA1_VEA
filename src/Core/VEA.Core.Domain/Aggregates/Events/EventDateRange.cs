using VEA.Core.Domain.Common.Bases;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDateRange : ValueObject<DateRange>
{
    private EventDateRange(DateRange value) : base(value) { }
    
    public static Result<EventDateRange> Create(DateRange value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<EventDateRange>.Failure(validation.Errors)
            : Result<EventDateRange>.Success(new EventDateRange(value));
    }
    
    public static Result Validate(DateRange value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}