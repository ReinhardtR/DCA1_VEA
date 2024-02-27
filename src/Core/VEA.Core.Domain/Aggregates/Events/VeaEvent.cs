namespace VEA.Core.Domain;

public class VeaEvent
{
    internal EventId Id;
    internal EventTitle Title;
    internal EventDescription Description;
    internal EventStatus Status;
    internal EventGuestLimit GuestLimit;
    internal DateRange DateRange;

    public VeaEvent(EventId id, EventTitle title, EventDescription description, EventStatus status, EventGuestLimit guestLimit, DateRange dateRange)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        GuestLimit = guestLimit;
        DateRange = dateRange;
    }
    
    
}
