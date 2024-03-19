using System.Data;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

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
    
    public static class Errors
    {
        public static Error GuestHasNoInvitation() =>
            new(ErrorType.InvalidOperation, 6, "This guest has no pending invitation");

        public static Error EventIsCancelled() =>
            new(ErrorType.InvalidOperation, 7, "The invitation can not be accepted as the event is cancelled");

        public static Error EventIsNotActive() =>
            new(ErrorType.InvalidOperation, 8, "The invitation can not be accepted as the event is not yet active");

        public static Error GuestLimitReached() =>
            new(ErrorType.InvalidOperation, 9, "The invitation cannot be accepted as the event has reached its guest limit");
    }
}