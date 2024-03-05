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
        var validation = Validate(value);
        return validation.IsFailure
            ? Result<ViaEmail>.Failure(validation.Errors)
            : Result<ViaEmail>.Success(new ViaEmail(value));
    }

    private static Result Validate(string value)
    {
        List<Error> errors = [];

        //Split value by "@" and check if the second part is "via.dk
        if (!value.Contains(Domainname))
            errors.Add(Errors.EmailDomainNotValid());
        
        return errors.Count > 0 ? Result.Failure(errors) : Result.Success();
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
    