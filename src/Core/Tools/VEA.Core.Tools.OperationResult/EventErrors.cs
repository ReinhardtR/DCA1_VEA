namespace VEA.Core.Tools.OperationResult;

public class EventErrors
{
    public Error InvalidEmail() => 
        new Error(ErrorType.InvalidArgument, 1, "Email is wrong");

    public static Error InvalidName() =>
        new Error(ErrorType.InvalidArgument, 2, "Name is wrong");
}