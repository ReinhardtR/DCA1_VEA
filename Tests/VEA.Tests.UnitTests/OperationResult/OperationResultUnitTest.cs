using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private record TestPayload(string Name, int Age);

    [Fact]
    public void Test1()
    {
        var none = Result<None>.Success(new None());
        var payload = Result<TestPayload>.Success(new TestPayload("John", 25));
    }
}