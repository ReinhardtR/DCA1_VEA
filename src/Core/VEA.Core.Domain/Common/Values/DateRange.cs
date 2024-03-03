namespace VEA.Core.Domain.Common.Values;

public class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }
    
    public DateRange(DateTime start, DateTime end)
    {
        Start = start;
        End = end;
    }
}