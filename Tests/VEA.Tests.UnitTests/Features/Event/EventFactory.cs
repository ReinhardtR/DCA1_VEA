using VEA.Core.Domain;

namespace VEA.Tests.UnitTests;

public class EventFactory
{
    private EventId _id;
    private EventTitle _title;
    private EventDescription _description;
    private EventStatus _status;
    private EventGuestLimit _guestLimit;
    private DateRange _dateRange;

    public static EventFactory Create()
    {
        return new EventFactory();
    }

    public EventFactory WithId(EventId id)
    {
        _id = id;
        return this;
    }

    public EventFactory WithTitle(EventTitle title)
    {
        _title = title;
        return this;
    }

    public EventFactory WithDescription(EventDescription description)
    {
        _description = description;
        return this;
    }

    public EventFactory WithStatus(EventStatus status)
    {
        _status = status;
        return this;
    }

    public EventFactory WithGuestLimit(EventGuestLimit guestLimit)
    {
        _guestLimit = guestLimit;
        return this;
    }

    public EventFactory WithDateRange(DateRange dateRange)
    {
        _dateRange = dateRange;
        return this;
    }

    public VeaEvent Build()
    {
        return VeaEvent.Create(_id, _title, _description, _status, _guestLimit, _dateRange);
    }
}
