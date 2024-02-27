namespace VEA.Core.Domain;

public class EventVisibility(int id, string name) : Enumeration(id, name)
{
  public readonly static EventVisibility Public = new(1, "Public");
  public readonly static EventVisibility Private = new(2, "Private");
}