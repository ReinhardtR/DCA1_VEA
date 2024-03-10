using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class Status(int id, string name) : Enumeration(id, name)
{
    public static readonly Status Pending = new(1, "Pending");
    public static readonly Status Declined = new(2, "Declined");
    public static readonly Status Accepted = new(3, "Accepted");
}