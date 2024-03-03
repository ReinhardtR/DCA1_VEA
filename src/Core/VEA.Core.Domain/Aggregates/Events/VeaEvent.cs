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
        // Waiting for EventStatus implementation

        // if (Status == EventStatus.Cancelled)
        //     //Add error
        //
        // if (Status == EventStatus.Started)
        //     //Add error

        Description = description;

        return Result.Success();
    }
    
    public Result UpdateDateRange(EventDateRange dateRange)
    {
        List<Error> errors = new List<Error>();

        if (errors.Count > 0)
            return Result.Failure(errors);

        DateRange = dateRange;
        Status = EventStatus.Draft;
        return Result.Success();
    }

    public Result SetVisibility(EventVisibility newVisibility)
    {
        List<Error> errors = [];

        if (Status == EventStatus.Cancelled)
            errors.Add(EventErrors.SetVisibilityWhenEventCancelled());

        if (Status == EventStatus.Active)
            errors.Add(EventErrors.SetVisibilityToPrivateWhenEventActive());

        if (errors.Count > 0)
            return Result.Failure(errors);

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
        List<Error> errors = new List<Error>();

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
        
        if (errors.Count > 0)
            return Result.Failure(errors);

        Status = EventStatus.Ready;
        return Result.Success();
    }
}
