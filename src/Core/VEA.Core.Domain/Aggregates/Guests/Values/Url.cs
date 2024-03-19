using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class Url : ValueObject<string>
{
    public Url(string value) : base(value){}
    
    public static Result<Url> Create(string value)
    {
        var validation = Validate(value);
        
        
        return validation.IsFailure 
            ? Result.Failure<Url>(validation.Errors)
            : Result.Success<Url>(new Url(value));
    }
    
    private static Result Validate(string value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}