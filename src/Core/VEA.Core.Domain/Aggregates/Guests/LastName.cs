using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class LastName : ValueObject<string>
{
    public LastName(string value) : base(value) {}
    
    public static Result<LastName> Create(string value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<LastName>.Failure(validation.Errors)
            : Result<LastName>.Success(new LastName(value));
    }
    
    private static Result Validate(string value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}