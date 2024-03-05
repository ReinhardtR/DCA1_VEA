﻿namespace VEA.Core.Tools.OperationResult;

public static class EventErrors
{
    public static Error EventMustHaveValidTitle() =>
        new(ErrorType.InvalidOperation, 13, "Event must have a valid title");
    public static Error VisibilityMustBeSet() =>
        new(ErrorType.InvalidOperation, 14, "Visibility must be set");
    public static Error EventMustBeDraft() =>
        new(ErrorType.InvalidOperation, 10, "Event must be in draft status");
    
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
    
    public static class DateRange
    {
        public static Error DateRangeStartDateMustBeBeforeEndDate() =>
            new(ErrorType.InvalidArgument, 15, "DateRange start date must be before end date");
        public static Error DateRangeStartTimeMustBeBeforeEndTimeWhenSameDate() =>
            new(ErrorType.InvalidArgument, 16, "DateRange start time must be before end time when on the same date");
        public static Error DateRangeStartTimeMustBeMinimumDurationBeforeEndTime() =>
            new(ErrorType.InvalidArgument, 17, "DateRange start time must be minimum duration before end time");
        public static Error DateRangeStartTimeMustBeAfterEarliestTime() =>
            new(ErrorType.InvalidArgument, 18, "DateRange start time must be after earliest time");
        public static Error DateRangeEndTimeMustBeBeforeLatestTime() =>
            new(ErrorType.InvalidArgument, 19, "DateRange start time must be before latest time");
        public static Error UpdateDateRangeWhenEventActive() =>
            new(ErrorType.InvalidArgument, 20, "Cannot update DateRange when event is active");
        public static Error DateRangeDurationExceedsMaximum() =>
            new(ErrorType.InvalidArgument, 21, "DateRange duration exceeds the maximum duration");
        public static Error EventStartTimeCannotBeInPast() =>
            new(ErrorType.InvalidArgument, 22, "Event start time cannot be in past");
        public static Error DateRangeSpansBetweenLatestAndEarliestTime() =>
            new(ErrorType.InvalidArgument, 23, "DateRange cannot span between the latest and earliest time");
    }
}