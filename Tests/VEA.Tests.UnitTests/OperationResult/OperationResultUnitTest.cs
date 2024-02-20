using VEA.Core.Tools.OperationResult;
using VEA.Core.Tools.OperationResult.DummyClasses;
using Xunit.Abstractions;
using Task = VEA.Core.Tools.OperationResult.DummyClasses.Task;

namespace VEA.Tests.UnitTests;

public class OperationResultUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    private record TestPayload(string Name, int Age);
    
    //Initialize Task Class
    private Task _testTask = new Task("Test Title");

    public OperationResultUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Returns_Nothing_CannotFail()
    {
        Result<None> result = _testTask.ReadyToStart();
    }
    
    [Fact]
    public void Returns_Nothing_CanFail()
    {
        Result<None> result = _testTask.SetTitle("t!");
        
        //Assert result is a success
        Assert.True(result.IsFailure);
        Assert.True(result.Payload == default);
        Assert.True(result.Errors.Count == 1);
    }
    
    [Fact]
    public void Returns_Something_CanFail_WithMultipleErrors()
    {
        Result<None> result = _testTask.SetTitle("to");
        
        //Assert result is a success
        Assert.True(result.IsFailure);
        Assert.True(result.Payload == default);
        Assert.True(result.Errors.Count == 2);
    }
    
    
    [Fact]
    public void Returns_Something_CannotFail()
    {
        Result<SubTask> result = _testTask.CreateSubTask_Cannot_Fail("Cannot!", 2);
        
        //Assert result is a success
        Assert.True(!result.IsFailure);
        Assert.True(result.Errors.Any() == false);
        Assert.True(result.Payload.Title == "Cannot!");
    }
    
    [Fact]
    public void Returns_Something_CanFail()
    {
       Result<SubTask> result = _testTask.CreateSubTask_Can_Fail("Cannot", 2);
        
        //Assert result is a success
        Assert.True(result.IsFailure);
        Assert.True(result.Payload == default);
        Assert.True(result.Errors.Count == 1);
    }
}