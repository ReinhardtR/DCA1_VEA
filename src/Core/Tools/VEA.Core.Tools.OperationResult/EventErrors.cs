namespace VEA.Core.Tools.OperationResult;

public class EventErrors
{
    public static Error InvalidEmail() =>
        new(ErrorType.InvalidArgument, 1, "Email is wrong");

    public static Error InvalidName() =>
        new(ErrorType.InvalidArgument, 2, "Name is wrong");
    
    //Description errors all start with 2
    public static Error DescriptionCannotBeLongerThan250Characters() =>
        new(ErrorType.InvalidArgument, 3, "Description cannot be longer than 250 characters");
    
    public static Error DescriptionCannotBeModifiedForCanceledEvent() =>
        new(ErrorType.InvalidOperation, 4, "Description cannot be modified for canceled event");

    public static Error DescriptionCannotBeEmpty() => 
        new(ErrorType.InvalidArgument, 5, "Description cannot be empty");
}