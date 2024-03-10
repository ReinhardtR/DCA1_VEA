using System.Data;
using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Core.Domain.Aggregates.Events;

public class Invitation
{
    internal InvitationId Id;
    internal InvitationStatus InvitationStatus;
    internal GuestId GuestId;
    
    private Invitation(InvitationId id, InvitationStatus invitationStatus, GuestId guestId)
    {
        Id = id;
        InvitationStatus = invitationStatus;
        GuestId = guestId;
    }
    
    public static Invitation Create(InvitationId id, InvitationStatus? status, GuestId guestId)
    {
        return new Invitation(
            id,
            status ?? InvitationStatus.Pending,
            guestId);
    }

    public void Accept()
    {
        InvitationStatus = InvitationStatus.Accepted;
    }

    public void Decline()
    {
        InvitationStatus = InvitationStatus.Declined;
    }
}