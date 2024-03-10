using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Core.Domain.Aggregates.Events;

public class Participation
{
    internal GuestId GuestId;
    internal InvitationsStatus InvitationsStatus;
    
    private Participation(GuestId guestId, InvitationsStatus invitationsStatus)
    {
        GuestId = guestId;
        InvitationsStatus = invitationsStatus;
    }
    
    public static Participation Create(GuestId guestId, InvitationsStatus? status)
    {
        return new Participation(
            guestId,
            status ?? InvitationsStatus.Pending);
    }
}