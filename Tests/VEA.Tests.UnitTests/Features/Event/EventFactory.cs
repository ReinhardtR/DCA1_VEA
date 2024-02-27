using VEA.Core.Domain;

namespace VEA.Tests.UnitTests;

public class EventFactory
{
    private EventId _id;
    private EventTitle _title;


    public static EventFactory Create()
    {
        return new EventFactory();
    }

    public EventFactory WithId(Guid id)
    {
        _id = EventId.Create(id).Payload;
        return this;
    }

    public EventFactory WithTitle(string title)
    {
        _title = EventTitle.Create(title).Payload;
        return this;
    }
    

    public VeaEvent Build()
    {
        return VeaEvent.Create(_id, _title);
    }
}
