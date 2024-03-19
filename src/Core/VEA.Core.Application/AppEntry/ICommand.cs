namespace VEA.Core.Application.AppEntry;

public abstract class ICommand
{
    // with only create method
    public static T Create<T>() where T : ICommand, new()
    {
        return new T();
    }
}