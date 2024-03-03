using VEA.Core.Domain;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.UpdateGuestLimit;

public class UpdateGuestLimitAggregateTest
{

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void UpdateGuestLimitWhenGuestLimitIsMoreThan5_WhenGuestLimitIsUpdated_ShouldUpdateGuestLimit(int input)
    {
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(input).Payload;
        var _event = EventFactory.Create().Build();

        // Act
        _event.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.Equal(newGuestLimit, _event.GuestLimit);

    }
    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void UpdateGuestLimitWhenGuestLimitIsMoreThan50_WhenGuestLimitIsUpdated_ShouldUpdateGuestLimit(int input)
    {
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(input).Payload;
        var _event = EventFactory.Create().Build();

        // Act
        _event.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.Equal(newGuestLimit, _event.GuestLimit);

    }

    //S3 TODO - depends on UC 8


    //F1 Given an existing event with ID And the event is in active status When creator reduces the number of maximum guests Then a failure message is provided explaining the maximum number of guests of an active cannot be reduced (it may only be increased)
    // TODO - depends on UC 8
    [Fact]
    public void GivenEventExistWithIdAndIsReadyStatus_WhenCreatorReducesTheNumberOfMaximumGuests_ThenFailureMessageIsProvided()
    {
        /*
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(5).Payload;
        var _event = EventFactory.Create().WithId().WithTitle("Event").Build();
        _event.UpdateStatus(EventStatus.Ready);

        // Act
        var result = _event.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.GuestLimitMustBeBetween5And50(), result.Errors);
        */
        Assert.True(false);
    }

    //F2 Given an existing event with ID And the event is in cancelled status When creator sets the number of maximum guests Then a failure message is provided explaining a cancelled event cannot be modified
    // TODO - depends on UC 8

    //F3 TODO - depends on UC 16-20

    //F4 Given an existing event with ID When creator sets the number of maximum guests to number < 5 Then a failure message is provided explaining the maximum number of guests cannot be negative

    [Fact]
    public void GivenEventExistWithId_WhenCreatorSetsTheNumberOfMaximumGuestsToNumberLessThan5_ThenFailureMessageIsProvided()
    {
        // Arrange

        // Act
        var result = EventGuestLimit.Create(4);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.GuestLimitMustBeBetween5And50(), result.Errors);
    }

    //F5 Given an existing event with ID When creator sets the number of maximum guests to a number > 50 Then a failure message is provided explaining the maximum number of guests cannot exceed 50
    [Fact]
    public void GivenEventExistWithId_WhenCreatorSetsTheNumberOfMaximumGuestsToNumberMoreThan50_ThenFailureMessageIsProvided()
    {
        // Arrange

        // Act
        var result = EventGuestLimit.Create(51);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.GuestLimitMustBeBetween5And50(), result.Errors);
    }
}