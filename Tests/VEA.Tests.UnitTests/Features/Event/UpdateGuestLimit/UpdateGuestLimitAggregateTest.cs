using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.UpdateGuestLimit;

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

    //S3
    // Given an existing event with ID
    // And the event is in active status
    // When creator sets the maximum number of guests
    // And the number is between 5 and 50 (both inclusive)
    // And the number of guests is larger than or equal to the previous value
    // Then the maximum number of guests is set to the selected value
    [Fact]
    public void GivenEventExistWithIdAndIsReadyStatus_WhenCreatorSetsTheNumberOfMaximumGuestsAndTheNumberIsBetween5And50AndTheNumberOfGuestsIsLargerThanOrEqualToThePreviousValue_ThenTheMaximumNumberOfGuestsIsSetToTheSelectedValue()
    {
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(30).Payload;
        var veaEvent = EventFactory.Create().WithStatus(EventStatus.Active).WithGuestLimit(25).Build();
        veaEvent.Ready();

        // Act
        Result result = veaEvent.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.Equal(newGuestLimit, veaEvent.GuestLimit);
    }


    //F1 Given an existing event with ID And the event is in active status When creator reduces the number of maximum guests Then a failure message is provided explaining the maximum number of guests of an active cannot be reduced (it may only be increased)
    [Fact]
    public void GivenEventExistWithIdAndIsReadyStatus_WhenCreatorReducesTheNumberOfMaximumGuests_ThenFailureMessageIsProvided()
    {
        
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(15).Payload;
        var veaEvent = EventFactory.Create().WithStatus(EventStatus.Active).WithGuestLimit(20).Build();
        veaEvent.Ready();

        // Act
        var result = veaEvent.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventGuestLimit.Errors.CannotUpdateGuestLimitWhenEventActive(), result.Errors);
    }

    //F2 Given an existing event with ID And the event is in cancelled status When creator sets the number of maximum guests Then a failure message is provided explaining a cancelled event cannot be modified
    [Fact]
    public void GivenEventExistWithIdAndIsCancelledStatus_WhenCreatorSetsTheNumberOfMaximumGuests_ThenFailureMessageIsProvided()
    {
        // Arrange
        var newGuestLimit = EventGuestLimit.Create(3).Payload;
        var veaEvent = EventFactory.Create().WithStatus(EventStatus.Cancelled).Build();

        // Act
        var result = veaEvent.UpdateGuestLimit(newGuestLimit);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventGuestLimit.Errors.CannotUpdateGuestLimitWhenEventCancelled(), result.Errors);
    }

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
        Assert.Contains(EventGuestLimit.Errors.GuestLimitMustBeBetween5And50(), result.Errors);
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
        Assert.Contains(EventGuestLimit.Errors.GuestLimitMustBeBetween5And50(), result.Errors);
    }
}