using System.Data.Common;
using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class ParticipationStatus(int id, string name) : Enumeration(id, name)
{
    public static readonly ParticipationStatus Participating = new(1, "Pending");
    public static readonly ParticipationStatus Cancelled = new(2, "Declined");
}