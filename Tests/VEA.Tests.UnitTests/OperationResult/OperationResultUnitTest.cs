using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private record TestPayload(string Name, int Age);
    
    //Initialize Task Class
    private Task testTask = new Task("Test Task");
    
    [Fact]
    public void Returns_Nothing_CannotFail()
    {
        //Return Void?
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