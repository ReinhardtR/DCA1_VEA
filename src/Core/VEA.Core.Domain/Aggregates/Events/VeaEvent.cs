namespace VEA.Core.Domain;

public class VeaEvent
{
    internal EventId Id;
    internal EventTitle Title;
    internal EventDescription Description;
    internal EventStatus Status;
    internal EventGuestLimit GuestLimit;
    internal DateRange DateRange;

    private VeaEvent(EventId id, EventTitle title, EventDescription description, EventStatus status, EventGuestLimit guestLimit, DateRange dateRange)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        GuestLimit = guestLimit;
        DateRange = dateRange;
    }
    
    public static VeaEvent Create(EventId id, EventTitle title, EventDescription description, EventStatus status, EventGuestLimit guestLimit, DateRange dateRange)
    {
        return new VeaEvent(id, title, description, status, guestLimit, dateRange);
    }
    
    
}
