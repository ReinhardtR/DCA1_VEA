namespace VEA.Core.Domain;

public class VeaEvent
{
    internal EventId Id;
    internal EventTitle Title;
    internal EventDescription Description;
    internal EventStatus Status;
    internal EventGuestLimit GuestLimit;
    internal DateRange DateRange;

    private VeaEvent(EventId id, EventTitle title)
    {
        Id = id;
        Title = title;

    }
    
    public static VeaEvent Create(EventId id, EventTitle title)
    {
        return new VeaEvent(id, title);
    }
    
    public void UpdateTitle(EventTitle title)
    {
        Title = title;
    }
}
