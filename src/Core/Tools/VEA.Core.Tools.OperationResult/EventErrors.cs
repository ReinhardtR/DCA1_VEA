namespace VEA.Core.Tools.OperationResult;

public class EventErrors
{
    public static Error InvalidEmail() =>
        new(ErrorType.InvalidArgument, 1, "Email is wrong");

    public static Error InvalidName() =>
        new(ErrorType.InvalidArgument, 2, "Name is wrong");
    

    //Description
    public static Error DescriptionCannotBeLongerThan250Characters() =>
        new(ErrorType.InvalidArgument, 3, "Description cannot be longer than 250 characters");
    
    public static Error DescriptionCannotBeModifiedForCanceledEvent() =>
        new(ErrorType.InvalidOperation, 4, "Description cannot be modified for canceled event");

    public static Error DescriptionCannotBeEmpty() => 
        new(ErrorType.InvalidArgument, 5, "Description cannot be empty");
    
    //Title
    public static Error TitleMustBeBetween3And75Characters() =>
        new(ErrorType.InvalidArgument, 6, "Title must be between 3 and 75 characters");
    
    public static Error UpdateTitleWhenEventActive() =>
        new(ErrorType.InvalidArgument, 7, "Cannot update title when event is active");
    
    public static Error UpdateTitleWhenEventCancelled() =>
        new(ErrorType.InvalidArgument, 8, "Cannot update title when event is cancelled");

    public static Error GuestLimitMustBeBetween5And50() =>
        new(ErrorType.InvalidArgument, 9, "Guest limit must be between 5 and 50");

    public static Error EventMustBeDraft() =>
        new(ErrorType.InvalidOperation, 10, "Event must be in draft status");
    
}