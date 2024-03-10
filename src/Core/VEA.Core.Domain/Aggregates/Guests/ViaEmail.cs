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
            .Assert(() => EmailIsCorrectFormat(value), Errors.EmailFormatNotValid())
            .Assert(() => EmailEndsWithVia(value), Errors.EmailDomainNotValid())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<ViaEmail>(validation.Errors)
            : Result.Success(new ViaEmail(value));
    }

    private static bool EmailEndsWithVia(string value)
    {
        return value.EndsWith(Domainname);
    }


    private static bool EmailIsCorrectFormat(string value)
    {
        //Email must must be like this: text1@text2.text3
        
        //If the count of @ is not 1, or if the count of . is not 1, then the email is not valid.
        if (value.Count(x => x == '@') != 1 || value.Count(x => x == '.') != 1)
        {
            return false;
        }
        
        //If the text1 of the email. Is not 4 letters of the english alphabet, or 6 numbers, then the email is not valid.
        var text1 = value.Split('@')[0];
        
        switch (text1)
        {
            case string s when s.Length == 3 || s.Length == 4 && s.All(char.IsLetter):
                return true;
            case string s when s.Length == 6 && s.All(char.IsDigit):
                return true;
            default:
                return false;
        }
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
        public static Error EmailAlreadyInUse() =>
            new(ErrorType.InvalidArgument, 5, "Email is already in use.");
    }
}
    