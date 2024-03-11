using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Common.Values;

namespace VEA.Tests.UnitTests.Features.Event;

public class EventFactory
{
    private EventId _id = EventId.New().Payload;
    private EventTitle? _title;
    private EventDescription? _description;
    private EventVisibility? _visibility;
    private EventStatus? _status;
    private EventGuestLimit? _guestLimit;
    private EventDateRange _dateRange;
    private List<Invitation>? _invitations;
    private List<Participation>? _participations;

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
    
    public EventFactory WithDateRange(DateRange dateRange)
    {
        _dateRange = EventDateRange.Create(dateRange).Payload;
        return this;
    }
    
    public EventFactory WithInvitations(List<Invitation> invitations)
    {
        _invitations = invitations;
        return this;
    }
    
    public EventFactory WithParticipations(List<Participation> participations)
    {
        _participations = participations;
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
            _guestLimit,
            _dateRange,
            _invitations,
            _participations
        );
    }
}
