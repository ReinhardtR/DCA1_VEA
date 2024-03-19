using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class FirstName : ValueObject<string>
{
    public FirstName(string value) : base(value) {}
    
    public static Result<FirstName> Create(string value)
    {
        var validation = Result.Validator()
            .Assert(value.Length >= 2 && value.Length <= 25, Errors.FirstNameNotValid())
            .Assert(value.All(char.IsLetter), Errors.FirstNameCannotContainNumbers())
            .Assert(value.All(char.IsLetterOrDigit), Errors.FirstNameCannotContainSymbols())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<FirstName>(validation.Errors)
            : Result.Success(new FirstName(value));
    }

    public class Errors
    {
        public static Error FirstNameNotValid() => 
            new(ErrorType.InvalidArgument, 1, "First name is not valid, must be between 2 and 25 characters.");

        public static Error FirstNameCannotContainNumbers() => 
            new(ErrorType.InvalidArgument, 2, "First name cannot contain numbers.");

        public static Error FirstNameCannotContainSymbols() => 
            new(ErrorType.InvalidArgument, 3, "First name cannot contain symbols.");
    }
}