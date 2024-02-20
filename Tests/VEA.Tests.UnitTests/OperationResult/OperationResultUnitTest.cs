using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private record TestPayload(string Name, int Age);

    [Fact]
    public void Returns_Nothing_CannotFail()
    {
        Result<None>.Success();
    }
    
    [Fact]
    public void Returns_Nothing_CanFail()
    {
        Result<None>.Failure("Error message");
    }
    
    [Fact]
    public void Returns_Something_CannotFail()
    {
        Result<TestPayload>.Success(new TestPayload("John", 25));
    }
    
    [Fact]
    public void Returns_Something_CanFail()
    {
        Result<TestPayload>.Failure("Error message");
    }
    
    [Fact]
    public void Returns_Something_CanFail_WithMultipleErrors()
    {
        // TBD
    }
}