using VEA.Core.Domain;

namespace VEA.Tests.UnitTests;

public class EventFactory
{
    private EventId _id = EventId.New().Payload;
    private EventTitle? _title;
    private EventDescription? _description;
    private EventVisibility? _visibility;
    private EventStatus? _status;
    private EventGuestLimit? _guestLimit;

    public static EventFactory Create()
    {
        return new EventFactory();
    }

    public EventFactory WithId(Guid id)
    {
        _id = new EventId(id);
        return this;
    }

    public EventFactory WithTitle(string title)
    {
        _title = EventTitle.Create(title).Payload;
        return this;
    }

    public EventFactory WithDescription(string description)
    {
        _description = EventDescription.Create(description).Payload;
        return this;
    }

    public EventFactory WithVisibility(EventVisibility visibility)
    {
        _visibility = visibility;
        return this;
    }

    public EventFactory WithStatus(EventStatus status)
    {
        _status = status;
        return this;
    }

    public EventFactory WithGuestLimit(int limit)
    {
        _guestLimit = EventGuestLimit.Create(limit).Payload;
        return this;
    }

    public VeaEvent Build()
    {
        return VeaEvent.Create(
            _id,
            _title,
            _description,
            _visibility,
            _status,
            _guestLimit
        );
    }
}
