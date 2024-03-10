using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Core.Domain.Aggregates.Events;

public class Participation
{
    internal GuestId GuestId;
    
    private Participation(GuestId guestId)
    {
        GuestId = guestId;
    }
    
    public static Participation Create(GuestId guestId)
    {
        return new Participation(guestId);
    }
}