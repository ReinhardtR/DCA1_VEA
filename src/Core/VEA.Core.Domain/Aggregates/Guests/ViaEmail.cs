using VEA.Core.Domain.Common.Bases;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public class ViaEmail : ValueObject<string>
{
    public ViaEmail(string value) : base(value) { }

    public const int MinNameLength = 3;
    public const int MaxNameLength = 6;

    private const string Domainname = "@via.dk";

    public static Result<ViaEmail> Create(string value)
    {
        var validation = Result.Validator()
            .Assert(!value.Contains(Domainname), Errors.EmailDomainNotValid())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<ViaEmail>(validation.Errors)
            : Result.Success(new ViaEmail(value));
    }
    
    public static class Errors
    {
        public static Error EmailDomainNotValid() =>
            new(ErrorType.InvalidArgument, 1, $"Email Domain must end with {Domainname}");
        public static Error EmailFormatNotValid() => 
            new (ErrorType.InvalidArgument, 2, "Email format is not valid, it must be \"*Name*@*Domain*.*Countrycode*\"");
        public static Error NameLengthNotValid() =>
            new(ErrorType.InvalidArgument, 3, $"Name length is not valid, must be between {MinNameLength} and {MaxNameLength}");
        public static Error EmailNameNotValid() => 
            new(ErrorType.InvalidArgument, 4, "Email name is not valid, must contain 3-4 letters, or 6 numbers.");
    }
}
    