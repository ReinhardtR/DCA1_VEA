using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventStatus(int id, string name) : Enumeration(id, name)
{
    public static readonly EventStatus Draft = new(1, "Draft");
    public static readonly EventStatus Ready = new(2, "Ready");
    public static readonly EventStatus Active = new(3, "Active");
    public static readonly EventStatus Cancelled = new(4, "Cancelled");
}