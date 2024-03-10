using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class Participation
{
    internal ParticipationId Id;
    internal GuestId GuestId;
    internal ParticipationStatus InvitationsStatus;
    
    
    private Participation(ParticipationId id, GuestId guestId, ParticipationStatus invitationsStatus)
    {
        Id = id;
        GuestId = guestId;
        InvitationsStatus = invitationsStatus;
    }
    
    
    public static Participation Create(ParticipationId id, GuestId guestId, ParticipationStatus? status)
    {
        return new Participation(
            id,
            guestId,
            status ?? ParticipationStatus.Participating);
    }
    
    public void Cancel()
    {
        InvitationsStatus = ParticipationStatus.Cancelled;
    }
}