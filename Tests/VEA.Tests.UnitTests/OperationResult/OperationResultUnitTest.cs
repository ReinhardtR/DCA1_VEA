using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private record TestPayload(string Name, int Age);

    [Fact]
    public void Test1()
    {
        Result none = Result.Success();
        Result something = Result.Success(new TestPayload("John", 25));
    }
}