using VEA.Core.Domain.Common.Bases;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventDateRange : ValueObject<DateRange>
{
    private EventDateRange(DateRange value) : base(value) => Value = value;
    
    private static readonly int MinDurationMinutes = 60;
    private static readonly int MaxDurationMinutes = 600;
    private static readonly TimeOnly EarliestStartTime = new TimeOnly(8, 0);
    private static readonly TimeOnly LatestEndTime = new TimeOnly(1, 0);
    
    public static Result<EventDateRange> Create(DateRange value)
    {
        var validation = Validate(value);
        return validation.IsFailure 
            ? Result<EventDateRange>.Failure(validation.Errors)
            : Result<EventDateRange>.Success(new EventDateRange(value));
    }
    
    public static Result Validate(DateRange value)
    {
        List<Error> errors = new List<Error>();
        DateOnly startDateOnly = DateOnly.FromDateTime(value.Start);
        DateOnly endDateOnly = DateOnly.FromDateTime(value.End);
        TimeOnly startTimeOnly = TimeOnly.FromDateTime(value.Start);
        TimeOnly endTimeOnly = TimeOnly.FromDateTime(value.End);
        
        // Start date must be before end date
        if(startDateOnly.CompareTo(endDateOnly)>=0)
            errors.Add(EventErrors.DateRangeStartDateMustBeBeforeEndDate());

        // If same date, start time must be before end time
        if(startDateOnly.CompareTo(endDateOnly)==0 && startTimeOnly.CompareTo(endTimeOnly)>0)
            errors.Add(EventErrors.DateRangeStartTimeMustBeBeforeEndTimeWhenSameDate());
        
        // Calculate the time difference in minutes
        double minutesDifference = (value.End - value.Start).TotalMinutes;
        
        // Duration must be lower than MaxDurationMinutes
        if (minutesDifference > MaxDurationMinutes)
            errors.Add(EventErrors.DateRangeDurationExceedsMaximum());

        // Start time must be MinDurationMinutes before end time
        if (minutesDifference == 0 || (minutesDifference < MinDurationMinutes && (startDateOnly.CompareTo(endDateOnly) != 0 || startTimeOnly.CompareTo(endTimeOnly) < 0)))
            errors.Add(EventErrors.DateRangeStartTimeMustBeMinimumDurationBeforeEndTime());
        
        // Start time must be before EarliestStartTime
        if (startTimeOnly.CompareTo(EarliestStartTime) < 0)
            errors.Add(EventErrors.DateRangeStartTimeMustBeAfterEarliestTime());

        // End time must be before LatestEndTime
        if (endTimeOnly.CompareTo(LatestEndTime)>0)
            errors.Add(EventErrors.DateRangeEndTimeMustBeBeforeLatestTime());
        
        // Start time must not be in the past
        if (value.Start < DateTime.Now)
            errors.Add(EventErrors.EventStartTimeCannotBeInPast());
        
        // Event must not span between latestEndTime and earliestStartTime
        if ((startDateOnly.AddDays(1).Equals(endDateOnly) || startDateOnly.Equals(endDateOnly) && startTimeOnly.CompareTo(EarliestStartTime) < 0) &&
            endTimeOnly.CompareTo(LatestEndTime) > 0)
        {
            errors.Add(EventErrors.DateRangeSpansBetweenLatestAndEarliestTime());
        }
        
        //If there are errors return a failure
        if (errors.Count > 0)
            return Result.Failure(errors);
        
        //If there are no errors return a success
        return Result.Success();
    }
}