using System.Reflection;

namespace VEA.Core.Domain.Common.Bases;

public abstract class Enumeration(int id, string name) : IComparable
{
  public int Id { get; private set; } = id;
  public string Name { get; private set; } = name;

  public override string ToString() => Name;

  public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
    typeof(T).GetFields(BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.DeclaredOnly)
             .Select(f => f.GetValue(null))
             .Cast<T>();

  public int CompareTo(object? other)
  {
    return other == null ? 1 : Id.CompareTo(((Enumeration)other).Id);
  }

  public override bool Equals(object? obj)
  {
    if (obj == null || obj.GetType() != GetType()) return false;
    return Id.Equals(((Enumeration)obj).Id);
  }

  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }
}
