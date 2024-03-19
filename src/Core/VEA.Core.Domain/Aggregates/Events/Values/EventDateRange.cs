using System.Runtime.InteropServices;
using VEA.Core.Domain.Common.Bases;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDateRange : ValueObject<DateRange>
{
    private EventDateRange(DateRange value) : base(value) { }

    private const int MinDurationMinutes = 60;
    private const int MaxDurationMinutes = 600;
    private const int EarliestStartTimeHours = 8;
    private const int LatestEndTimeHours = 1;
    
    public static Result<EventDateRange> Create(DateRange value)
    {
        var difference = (value.End - value.Start);
        var validation = Result.Validator()
            .Assert(value.Start.CompareTo(value.End)<=0, Errors.DateRangeStartMustBeBeforeEnd())
            .Assert(difference.TotalMinutes>=MinDurationMinutes, Errors.DateRangeStartTimeMustBeMinimumDurationBeforeEndTime())
            .Assert(difference.TotalMinutes<=MaxDurationMinutes, Errors.DateRangeDurationExceedsMaximum())
            .Assert(value.Start.Hour >= EarliestStartTimeHours, Errors.DateRangeStartTimeMustBeAfterEarliestTime())
            .Assert(value.End.Hour is > EarliestStartTimeHours or < LatestEndTimeHours, Errors.DateRangeEndTimeMustBeBeforeLatestTime())
            .Assert(!EventSpansBetweenLatestAndEarliestTimes(value.Start, value.End), Errors.DateRangeSpansBetweenLatestAndEarliestTime())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<EventDateRange>(validation.Errors)
            : Result.Success(new EventDateRange(value));
    }

    private static bool EventSpansBetweenLatestAndEarliestTimes(DateTime start, DateTime end)
    {
        var latestTimeSpan = new TimeSpan(1, 0, 0);
        var earliestTimeSpan = new TimeSpan(8, 0, 0);
        if (start.Date != end.Date)
        {
            return end.TimeOfDay >= latestTimeSpan;
        }

        return start.TimeOfDay <= earliestTimeSpan && latestTimeSpan <= end.TimeOfDay;
    }
    
    public static class Errors
    {
        public static Error DateRangeStartMustBeBeforeEnd() =>
            new(ErrorType.InvalidArgument, 1, "DateRange start date must be before end date");
        public static Error DateRangeStartTimeMustBeMinimumDurationBeforeEndTime() =>
            new(ErrorType.InvalidArgument, 2, "DateRange start time must be minimum duration before end time");
        public static Error DateRangeStartTimeMustBeAfterEarliestTime() =>
            new(ErrorType.InvalidArgument, 3, "DateRange start time must be after earliest time");
        public static Error DateRangeEndTimeMustBeBeforeLatestTime() =>
            new(ErrorType.InvalidArgument, 4, "DateRange start time must be before latest time");
        public static Error UpdateDateRangeWhenEventActive() =>
            new(ErrorType.InvalidArgument, 5, "Cannot update DateRange when event is active");
        public static Error UpdateDateRangeWhenEventCancelled() =>
            new(ErrorType.InvalidArgument, 6, "Cannot update DateRange when event is cancelled");
        public static Error DateRangeDurationExceedsMaximum() =>
            new(ErrorType.InvalidArgument, 7, "DateRange duration exceeds the maximum duration");
        public static Error EventStartTimeCannotBeInPast() =>
            new(ErrorType.InvalidArgument, 8, "Event start time cannot be in past");
        public static Error DateRangeSpansBetweenLatestAndEarliestTime() =>
            new(ErrorType.InvalidArgument, 9, "DateRange cannot span between the latest and earliest time");
    }
}