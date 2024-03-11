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
            .Assert(value.Start.CompareTo(value.End)<=0, EventErrors.DateRange.DateRangeStartMustBeBeforeEnd())
            .Assert(difference.TotalMinutes>=MinDurationMinutes, EventErrors.DateRange.DateRangeStartTimeMustBeMinimumDurationBeforeEndTime())
            .Assert(difference.TotalMinutes<=MaxDurationMinutes, EventErrors.DateRange.DateRangeDurationExceedsMaximum())
            .Assert(value.Start.Hour >= EarliestStartTimeHours, EventErrors.DateRange.DateRangeStartTimeMustBeAfterEarliestTime())
            .Assert(value.End.Hour is > EarliestStartTimeHours or < LatestEndTimeHours, EventErrors.DateRange.DateRangeEndTimeMustBeBeforeLatestTime())
            // TODO: @sebastian i added an "!" in front of this method, i think this is the intended behaviour? - Reinahrdt
            .Assert(!EventSpansBetweenLatestAndEarliestTimes(value.Start, value.End), EventErrors.DateRange.DateRangeSpansBetweenLatestAndEarliestTime())
            .Validate();
        return validation.IsFailure
            ? Result.Failure<EventDateRange>(validation.Errors)
            : Result.Success(new EventDateRange(value));
    }

    private static bool EventSpansBetweenLatestAndEarliestTimes(DateTime start, DateTime end)
    => (start.Date != end.Date && end.Hour > EarliestStartTimeHours) ||
       (start.Date == end.Date && start.Hour < LatestEndTimeHours && end.Hour < LatestEndTimeHours);
}