using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private record TestPayload(string Name, int Age);

    [Fact]
    public void Returns_Nothing_CannotFail()
    {
        //Return Void?
        Result none = Result.Success();
        Result something = Result.Success(new TestPayload("John", 25));
    }
    
    [Fact]
    public void Returns_Nothing_CanFail()
    {
        //Return Result<Void>? With a possible error?
    }
    
    [Fact]
    public void Returns_Something_CannotFail()
    {
        //Return T?
    }
    
    [Fact]
    public void Returns_Something_CanFail()
    {
        //Return Result<T>? With a possible error?
    }
    
    [Fact]
    public void Returns_Something_CanFail_WithMultipleErrors()
    {
        //Return Result<T>? With a possible error?
    }
}