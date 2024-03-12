namespace VEA.Core.Domain.Common;

public class CurrentTime : ICurrentTime
{
    public DateTime Now()
    {
        return DateTime.Now;
    }
}