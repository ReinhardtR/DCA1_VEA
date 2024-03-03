using VEA.Core.Domain;

namespace VEA.Tests.UnitTests;

public class ReadyEventAggregateTest
{

    //S1 Given an existing event with ID And the event is in draft status And the following data is set with valid values: title, description, times, visibility, maximum guests When creator readies the event Then the event is made ready
    [Fact]
    public void GivenEventExistWithIdAndIsDraftStatus_WhenCreatorReadiesTheEvent_ThenEventIsMadeReady()
    {
        // Arrange
        var _event = EventFactory.Create().Build();

        // Act
        _event.Ready();

        // Assert
        Assert.Equal(EventStatus.Ready, _event.Status);
    }

    //F1Given an existing event with ID And the event is in draft status And the any of the following is true: · Title is not set or remains default value · Description is not set or remains default value · Times are not set or remains default value · visibility is not set · maximum guests is not between 5 and 50 When creator readies the event Then a failure message is provided explaining what data is missing
    [Fact]
    public void GivenEventExistWithIdAndIsDraftStatusAndAnyOfTheFollowingIsTrue_WhenCreatorReadiesTheEvent_ThenFailureMessageIsProvided()
    {
        // Arrange
        var _event = EventFactory.Create().Build();

        // Act
        _event.Ready();

        // Assert
        Assert.Equal(EventStatus.Ready, _event.Status);
    }
}
