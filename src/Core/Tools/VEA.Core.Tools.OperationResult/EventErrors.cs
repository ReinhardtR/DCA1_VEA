namespace VEA.Core.Tools.OperationResult;

public class EventErrors
{
    public static Error InvalidEmail() =>
        new(ErrorType.InvalidArgument, 1, "Email is wrong");

    public static Error InvalidName() =>
        new(ErrorType.InvalidArgument, 2, "Name is wrong");
}