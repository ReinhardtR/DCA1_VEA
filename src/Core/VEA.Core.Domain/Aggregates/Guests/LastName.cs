using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class LastName : ValueObject<string>
{
    public LastName(string value) : base(value) {}
    
    public static Result<LastName> Create(string value)
    {
        var validation = Result.Validator()
            .Assert(value.Length >= 2 && value.Length <= 25, Errors.LastNameNotValid())
            .Assert(value.All(char.IsLetter), Errors.FirstNameCannotContainNumbers())
            .Assert(value.All(char.IsLetterOrDigit), Errors.FirstNameCannotContainSymbols())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<LastName>(validation.Errors)
            : Result.Success(new LastName(value));
    }

    public class Errors
    {
        public static Error LastNameNotValid() => 
            new(ErrorType.InvalidArgument, 1, "Last name is not valid, must be between 2 and 25 characters.");

        public static Error FirstNameCannotContainNumbers() => 
            new(ErrorType.InvalidArgument, 2, "Last name cannot contain numbers.");

        public static Error FirstNameCannotContainSymbols() => 
            new(ErrorType.InvalidArgument, 3, "Last name cannot contain symbols.");
    }
}