using System.Runtime.InteropServices.ComTypes;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public class VeaEvent
{
    internal EventId Id;
    internal EventTitle Title;
    internal EventDescription Description;
    internal EventVisibility Visibility;
    internal EventStatus Status;
    internal EventGuestLimit GuestLimit;
    internal EventDateRange? DateRange;
    internal List<Invitation> Invitations;
    internal List<GuestId> Participants;

    private VeaEvent(EventId id, EventTitle title, EventDescription description, EventVisibility visibility, EventStatus status, EventGuestLimit guestLimit, EventDateRange? dateRange, List<Invitation> invitations, List<GuestId> participants)
    {
        Id = id;
        Title = title;
        Description = description;
        Visibility = visibility;
        Status = status;
        GuestLimit = guestLimit;
        Invitations = invitations;
        DateRange = dateRange;
        Participants = participants;
    }

    public static VeaEvent Create(
        EventId id,
        EventTitle? eventTitle = null,
        EventDescription? eventDescription = null,
        EventVisibility? eventVisibility = null,
        EventStatus? eventStatus = null,
        EventGuestLimit? eventGuestLimit = null,
        EventDateRange? eventDateRange = null,
        List<Invitation>? invitations = null,
        List<GuestId>? participants = null
    )
    {
        return new VeaEvent(
            id,
            eventTitle ?? EventTitle.Create("Working Title").Payload,
            eventDescription ?? EventDescription.Create("").Payload,
            eventVisibility ?? EventVisibility.Private,
            eventStatus ?? EventStatus.Draft,
            eventGuestLimit ?? EventGuestLimit.Create(5).Payload,
            eventDateRange ?? null,
            invitations ?? [],
            participants ?? []
        );
    }

    public Result UpdateDescription(EventDescription description)
    {
        
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Active, EventDescription.Errors.CannotUpdateActiveEvent())
            .Assert(Status != EventStatus.Cancelled, EventDescription.Errors.CannotUpdateCancelledEvent())
            .Validate();
        
        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        //Else succeed
        Description = description;
        if (Status == EventStatus.Ready)
            Status = EventStatus.Draft;

        // Should set event status to draft.
        return Result.Success();
    }

    public Result UpdateDateRange(EventDateRange dateRange)
    {
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Active, EventDateRange.Errors.UpdateDateRangeWhenEventActive())
            .Assert(Status != EventStatus.Cancelled, EventDateRange.Errors.UpdateDateRangeWhenEventCancelled())
            .Validate();

        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        DateRange = dateRange;
        Status = EventStatus.Draft;

        return Result.Success();
    }

    public Result SetVisibility(EventVisibility newVisibility)
    {
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Cancelled, EventErrors.Visibility.SetVisibilityWhenEventCancelled())
            .Assert(Status != EventStatus.Active, EventErrors.Visibility.SetVisibilityToPrivateWhenEventActive())
            .Validate();
        if (validation.IsFailure) return validation;

        if (Status == EventStatus.Ready &&
            Visibility.Equals(EventVisibility.Public) &&
            newVisibility.Equals(EventVisibility.Private))
        {
            Status = EventStatus.Draft;
        }

        Visibility = newVisibility;

        return Result.Success();
    }

    public Result UpdateTitle(EventTitle title)
    {
        
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Active, EventTitle.Errors.UpdateTitleWhenEventActive())
            .Assert(Status != EventStatus.Cancelled, EventTitle.Errors.UpdateTitleWhenEventCancelled())
            .Validate();
        
        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        Title = title;
        return Result.Success();
    }

    public Result UpdateGuestLimit(EventGuestLimit guestLimit)
    {
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Active || GuestLimit.Value <= guestLimit.Value, EventGuestLimit.Errors.CannotUpdateGuestLimitWhenEventActive())
            .Assert(Status != EventStatus.Cancelled, EventGuestLimit.Errors.CannotUpdateGuestLimitWhenEventCancelled())
            .Validate();
        
        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        GuestLimit = guestLimit;
        return Result.Success();
    }

    public Result Ready()
    {
        var validation = Result.Validator()
            .Assert(Status == EventStatus.Draft, Errors.Event.EventMustBeDraft())
            .Assert(!Title.Value.Equals("Working Title"), Errors.Event.EventMustHaveValidTitle())
            .Assert(!Description.Value.Equals(""), EventDescription.Errors.DescriptionCannotBeEmpty())
            .Assert(DateRange?.Value.Start < DateRange?.Value.End,
                EventDateRange.Errors.DateRangeStartMustBeBeforeEnd())
            .Assert(DateRange?.Value.Start > DateTime.Now, EventDateRange.Errors.EventStartTimeCannotBeInPast())
            .Validate();
        
        validation = Result.Merge(validation, EventGuestLimit.Validate(GuestLimit.Value));
        validation = Result.Merge(validation, EventTitle.Validate(Title.Value));
        
        if (validation.IsFailure) return validation;

        Status = EventStatus.Ready;
        return Result.Success();
    }

    public Result Activate()
    {
       
        if (Status == EventStatus.Draft)
        {
            Result result = Ready();
            if (result.IsFailure)
                return Result.Failure(result.Errors);
        }
        
        var validation = Result.Validator()
            .Assert(Status != EventStatus.Cancelled, Errors.Event.EventCannotBeActivatedWhenCancelled())
            .Validate();
        
        if (validation.IsFailure) return validation;

        Status = EventStatus.Active;
        return Result.Success();
    }


    public Result Participate(GuestId guestId)
    {
        var validation = Result.Validator()
            .Assert(Status == EventStatus.Active, Errors.Participation.EventIsNotActive())
            .Assert(!GuestLimitReached(GuestLimit), Errors.Participation.GuestLimitReached())
            .Assert(EventHasNotStarted, Errors.Participation.EventAlreadyStarted())
            .Assert(Visibility == EventVisibility.Public, Errors.Participation.EventNotPublic())
            .Assert(!Participants.Contains(guestId), Errors.Participation.GuestAlreadyParticipated())
            .Validate();
        if (validation.IsFailure) return validation;

        Participants.Add(guestId);

        return Result.Success();
    }

    public Result CancelParticipation(GuestId guestId)
    {
        var validation = Result.Validator()
            .Assert(EventHasNotStarted, Errors.Participation.EventAlreadyStarted())
            .Validate();
        if (validation.IsFailure) return validation;

        Participants.Remove(guestId);

        return Result.Success();
    }

    private bool EventHasNotStarted()
    {
        if (DateRange == null)
            return true;

        return DateRange.Value.Start > DateTime.Now;
    }

    public Result ExtendInvitation(Invitation invitation)
    {
        var validation = Result.Validator()
            .Assert(!GuestLimitReached(GuestLimit), Errors.Invitation.GuestLimitReached())
            .Assert(Status != EventStatus.Cancelled && Status != EventStatus.Draft, EventErrors.Invitation.ExtendInvitationWhenEventDraftOrCancelled())
            .Validate();

        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        Invitations.Add(invitation);

        return Result.Success();
    }

    public Result AcceptInvitation(GuestId guestId)
    {
        var invitationForGuest = Invitations.FirstOrDefault(inv => inv.GuestId == guestId);

        var validation = Result.Validator()
            .Assert(invitationForGuest != null, Errors.Invitation.GuestHasNoInvitation())
            .Assert(!GuestLimitReached(GuestLimit), Errors.Invitation.GuestLimitReached())
            .Assert(Status != EventStatus.Cancelled, Errors.Invitation.EventIsCancelled())
            .Assert(Status != EventStatus.Ready, Errors.Invitation.EventIsNotActive())
            .Validate();

        if (validation.IsFailure)
            return Result.Failure(validation.Errors);

        invitationForGuest!.Accept();

        return Result.Success();
    }

    public Result DeclineInvitation(GuestId guestId)
    {
        var invitationForGuest = Invitations.FirstOrDefault(inv => inv.GuestId == guestId);

        var validation = Result.Validator()
            .Assert(invitationForGuest != null, Errors.Invitation.GuestHasNoInvitation())
            .Assert(Status != EventStatus.Cancelled, Errors.Invitation.EventIsCancelled())
            .Assert(Status == EventStatus.Active, Errors.Invitation.EventIsNotActive())
            .Validate();

        if (validation.IsFailure)
        {
            return Result.Failure(validation.Errors);
        }

        invitationForGuest!.Decline();

        return Result.Success();
    }

    private bool GuestLimitReached(EventGuestLimit guestLimit)
    {
        int actualGuests = Participants.Count
            + Invitations.Count(i => i.InvitationStatus == InvitationStatus.Accepted);
        return actualGuests >= guestLimit.Value;
    }

    public static class Errors
    {

        public static class Participation
        {
            public static Error EventIsNotActive() =>
               new(ErrorType.InvalidOperation, 1, "Event must be active to participate in it");

            public static Error GuestLimitReached() =>
               new(ErrorType.InvalidOperation, 2, "Guest limit has been reached, cannot participate");

            public static Error EventAlreadyStarted() =>
               new(ErrorType.InvalidOperation, 3, "Event has already started, cannot participate");

            public static Error EventNotPublic() =>
               new(ErrorType.InvalidOperation, 4, "Event is not public, cannot participate");

            public static Error GuestAlreadyParticipated() =>
               new(ErrorType.InvalidOperation, 5, "Guest has already participated in the event");
        }

        public static class Invitation
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
        public static class Event
        {
            public static Error EventMustBeDraft() =>
                new(ErrorType.InvalidOperation, 10, "Event must be in draft status to be made ready");

            public static Error EventMustHaveValidTitle() =>
                new(ErrorType.InvalidOperation, 11, "Event must have a valid title to be made ready");

            public static Error EventCannotBeActivatedWhenCancelled() =>
                new(ErrorType.InvalidOperation, 12, "Event cannot be activated when cancelled");
        }
    }
}
