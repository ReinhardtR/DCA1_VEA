using VEA.Core.Domain.Common.Bases;

namespace VEA.Core.Domain.Aggregates.Events;

public class EventVisibility(int id, string name) : Enumeration(id, name)
{
  public static readonly EventVisibility Public = new(1, "Public");
  public static readonly EventVisibility Private = new(2, "Private");
}