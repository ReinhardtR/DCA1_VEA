using System.Data;
using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Core.Domain.Aggregates.Events;

public class Invitation
{
    internal InvitationId Id;
    internal Status Status;
    internal GuestId GuestId;
    
    private Invitation(InvitationId id, Status status, GuestId guestId)
    {
        Id = id;
        Status = status;
        GuestId = guestId;
    }
    
    public static Invitation Create(InvitationId id, Status? status, GuestId guestId)
    {
        return new Invitation(
            id,
            status ?? Status.Pending,
            guestId);
    }

    public void Accept()
    {
        Status = Status.Accepted;
    }

    public void Decline()
    {
        Status = Status.Declined;
    }
}