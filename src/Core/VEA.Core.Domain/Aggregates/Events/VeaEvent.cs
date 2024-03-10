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

    private VeaEvent(EventId id, EventTitle title, EventDescription description, EventVisibility visibility, EventStatus status, EventGuestLimit guestLimit)
    {
        Id = id;
        Title = title;
        Description = description;
        Visibility = visibility;
        Status = status;
        GuestLimit = guestLimit;
    }

    public static VeaEvent Create(EventId id, EventTitle? eventTitle, EventDescription? eventDescription, EventVisibility? eventVisibility, EventStatus? eventStatus, EventGuestLimit? eventGuestLimit)
    {
        return new VeaEvent(
            id,
            eventTitle ?? EventTitle.Create("Working Title").Payload,
            eventDescription ?? EventDescription.Create("").Payload,
            eventVisibility ?? EventVisibility.Private,
            eventStatus ?? EventStatus.Draft,
            eventGuestLimit ?? EventGuestLimit.Create(5).Payload
        );
    }

    public Result UpdateDescription(EventDescription description)
    {
        List<Error> errors = new List<Error>();
        if (Status == EventStatus.Active)
            errors.Add(EventErrors.Description.CannotUpdateActiveEvent());

        if (Status == EventStatus.Cancelled) 
            errors.Add(EventErrors.Description.CannotUpdateCancelledEvent());
        
        if (errors.Count > 0)
            return Result.Failure(errors);
        
        //Else succeed
        Description = description;
        if (Status == EventStatus.Ready)
            Status = EventStatus.Draft;
        
        // Should set event status to draft.
        return Result.Success();
    }
    
    public Result UpdateDateRange(EventDateRange dateRange)
    {
        List<Error> errors = new List<Error>();
        
        if (Status == EventStatus.Active)
            errors.Add(EventErrors.DateRange.UpdateDateRangeWhenEventActive());
        
        if (errors.Count > 0)
            return Result.Failure(errors);

        DateRange = dateRange;
        Status = EventStatus.Draft;
        return Result.Success();
    }

    public Result SetVisibility(EventVisibility newVisibility)
    {
        var validation = Result.Validator()
            .Assert(Status == EventStatus.Cancelled, EventErrors.Visibility.SetVisibilityWhenEventCancelled())
            .Assert(() => Status == EventStatus.Active, EventErrors.Visibility.SetVisibilityToPrivateWhenEventActive())
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
        List<Error> errors = new();

        // Waiting for EventStatus implementation

        // if (Status == EventStatus.Active)
        //     //Add error
        //
        // if (Status == EventStatus.Started)
        //     //Add error

        if (errors.Count > 0)
            return Result.Failure(errors);

        Title = title;
        return Result.Success();
    }

    public Result UpdateGuestLimit(EventGuestLimit guestLimit)
    {
        List<Error> errors = new List<Error>();

        // Waiting for EventStatus implementation

        // if (Status == EventStatus.Active)
        //     //Add error


        if (errors.Count > 0)
            return Result.Failure(errors);

        GuestLimit = guestLimit;
        return Result.Success();
    }

    public Result Ready()
    {
        List<Error> errors = new List<Error>();
        if (Status != EventStatus.Draft)
            errors.Add(EventErrors.EventMustBeDraft());
        
        if (Title.Value.Equals("Working Title"))
            errors.Add(EventErrors.EventMustHaveValidTitle());
        
        if (Description.Value.Equals(""))
            errors.Add(EventErrors.Description.DescriptionCannotBeEmpty());
        
        // Waiting for EventDateRange implementation

        if (errors.Count > 0)
            return Result.Failure(errors);

        Status = EventStatus.Ready;
        return Result.Success();
    }

    public Result Activate()
    {
        List<Error> errors = new List<Error>();

        if (Status == EventStatus.Draft)
        {
            Result result = Ready();
            if (result.IsFailure)
                return Result.Failure(result.Errors);
        }
        
        if (Status == EventStatus.Cancelled)
            errors.Add(EventErrors.EventCannotBeActivatedWhenCancelled());
        
        if (errors.Count > 0)
            return Result.Failure(errors);
        
        Status = EventStatus.Active;
        return Result.Success();
    }

    public Result Participate(Guest guest)
    {


        return Result.Success();
    }
}
