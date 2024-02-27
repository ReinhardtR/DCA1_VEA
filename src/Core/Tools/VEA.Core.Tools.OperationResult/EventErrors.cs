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
    public static Error EmptyTitle() =>
        new(ErrorType.InvalidArgument, 3, "Title is empty");
    
    public static Error TooShortTitle() =>
        new(ErrorType.InvalidArgument, 4, "Title is too short");
    
    public static Error TooLongTitle() =>
        new(ErrorType.InvalidArgument, 5, "Title is too long");
    
    public static Error NullTitle() =>
        new(ErrorType.InvalidArgument, 6, "Title is null");
    
    public static Error UpdateTitleWhenEventActive() =>
        new(ErrorType.InvalidArgument, 7, "Cannot update title when event is active");
    
    public static Error UpdateTitleWhenEventCancelled() =>
        new(ErrorType.InvalidArgument, 8, "Cannot update title when event is cancelled");

}