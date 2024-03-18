namespace VEA.Core.Tools.OperationResult;

public static class EventErrors
{
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