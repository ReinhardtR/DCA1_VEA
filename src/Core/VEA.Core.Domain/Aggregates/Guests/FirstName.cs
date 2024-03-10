using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class FirstName : ValueObject<string>
{
    public FirstName(string value) : base(value) {}
    
    public static Result<FirstName> Create(string value)
    {
        var validation = Result.Validator().Validate();
        return validation.IsFailure
            ? Result.Failure<FirstName>(validation.Errors)
            : Result.Success(new FirstName(value));
    }
    
    private static Result Validate(string value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}