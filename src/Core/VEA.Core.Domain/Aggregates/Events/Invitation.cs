using System.Data;
using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Core.Domain.Aggregates.Events;

public class Invitation
{
    internal InvitationId Id;
    internal InvitationsStatus InvitationsStatus;
    internal GuestId GuestId;
    
    private Invitation(InvitationId id, InvitationsStatus invitationsStatus, GuestId guestId)
    {
        Id = id;
        InvitationsStatus = invitationsStatus;
        GuestId = guestId;
    }
    
    public static Invitation Create(InvitationId id, InvitationsStatus? status, GuestId guestId)
    {
        return new Invitation(
            id,
            status ?? InvitationsStatus.Pending,
            guestId);
    }

    public void Accept()
    {
        InvitationsStatus = InvitationsStatus.Accepted;
    }

    public void Decline()
    {
        InvitationsStatus = InvitationsStatus.Declined;
    }
}