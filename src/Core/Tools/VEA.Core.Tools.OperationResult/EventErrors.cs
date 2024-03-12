namespace VEA.Core.Tools.OperationResult;

public static class EventErrors
{
    public static Error EventMustHaveValidTitle() =>
        new(ErrorType.InvalidOperation, 13, "Event must have a valid title");
    public static Error VisibilityMustBeSet() =>
        new(ErrorType.InvalidOperation, 14, "Visibility must be set");
    public static Error EventMustBeDraft() =>
        new(ErrorType.InvalidOperation, 10, "Event must be in draft status");
    public static Error EventCannotBeActivatedWhenCancelled() =>
        new(ErrorType.InvalidOperation, 24, "Event cannot be activated when cancelled");
    public static class Title
    {
        public static Error TitleMustBeBetween3And75Characters() =>
            new(ErrorType.InvalidArgument, 6, "Title must be between 3 and 75 characters");
        public static Error UpdateTitleWhenEventActive() =>
            new(ErrorType.InvalidArgument, 7, "Cannot update title when event is active");
        public static Error UpdateTitleWhenEventCancelled() =>
            new(ErrorType.InvalidArgument, 8, "Cannot update title when event is cancelled");
    }
    
    public static class Description
    {
        public static Error DescriptionCannotBeLongerThan250Characters() =>
            new(ErrorType.InvalidArgument, 3, "Description cannot be longer than 250 characters");
        public static Error DescriptionCannotBeModifiedForCanceledEvent() =>
            new(ErrorType.InvalidOperation, 4, "Description cannot be modified for canceled event");
        public static Error DescriptionCannotBeEmpty() =>
            new(ErrorType.InvalidArgument, 5, "Description cannot be empty");

        public static Error CannotUpdateCancelledEvent() =>
            new(ErrorType.InvalidOperation, 24, "Cannot update cancelled event");

        public static Error CannotUpdateActiveEvent() => 
            new(ErrorType.InvalidOperation, 25, "Cannot update started event");
    }

    public static class GuestLimit
    {
        public static Error GuestLimitMustBeBetween5And50() =>
            new(ErrorType.InvalidArgument, 9, "Guest limit must be between 5 and 50");
    }
    
    public static class Visibility
    {
        public static Error SetVisibilityWhenEventCancelled() =>
            new(ErrorType.InvalidOperation, 11, "Cannot set visibility when event is cancelled");
        public static Error SetVisibilityToPrivateWhenEventActive() =>
            new(ErrorType.InvalidOperation, 12, "Cannot set visibility when event is active");
    }
    
    public static class Invitation
    {
        public static Error ExtendInvitationWhenEventDraftOrCancelled() =>
            new(ErrorType.InvalidArgument, 24, "Cannot invite guest when event status is draft or cancelled");
    }
}