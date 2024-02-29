using VEA.Core.Tools.OperationResult;

using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain;

public class VeaEvent
{
    internal EventId Id;
    internal EventTitle Title;
    internal EventDescription Description;
    internal EventStatus Status;
    internal EventGuestLimit GuestLimit;
    internal DateRange DateRange;

    private VeaEvent(EventId id, EventTitle title)
    {
        Id = id;
        Title = title;
        Status = EventStatus.Draft;
    }

    public static VeaEvent Create(EventId id, EventTitle title)
    {
        return new VeaEvent(id, title);
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

    public Result SetVisibility(EventVisibility visibility)
    {
        visibility = visibility;
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
        
        Status = EventStatus.Ready;
        return Result.Success();
    }
}
