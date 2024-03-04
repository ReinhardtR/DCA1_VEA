using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Common.Values;

public class Url : ValueObject<string>
{
    public Url(string value) : base(value){}
    
    public static Result<Url> Create(string value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<Url>.Failure(validation.Errors)
            : Result<Url>.Success(new Url(value));
    }
    
    private static Result Validate(string value)
    {
        List<Error> errors = [];
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
    }
}