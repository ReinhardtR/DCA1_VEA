using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class ViaEmail : ValueObject<string>
{
    public ViaEmail(string value) : base(value) {}
    
    public static Result<ViaEmail> Create(string value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<ViaEmail>.Failure(validation.Errors)
            : Result<ViaEmail>.Success(new ViaEmail(value));
    }

    private static Result Validate(string value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}