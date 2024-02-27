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
        _id = new EventId(id);
        return this;
    } 
    public EventFactory WithId()
    {
        _id = EventId.New().Payload;
        return this;
    }
    public EventFactory WithTitle(string title)
    {
        _title = EventTitle.create(title).Payload;
        return this;
    }
    

    public VeaEvent Build()
    {
        return VeaEvent.Create(_id, _title);
    }
}
