using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class LastName : ValueObject<string>
{
    public LastName(string value) : base(value) {}
    
    public static Result<LastName> Create(string value)
    {
        var validation = Result.Validator().Validate();
        return validation.IsFailure
            ? Result.Failure<LastName>(validation.Errors)
            : Result.Success(new LastName(value));
    }
}