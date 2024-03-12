using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.Ready;

public class ReadyEventAggregateTest
{
    //S1 Given an existing event with ID And the event is in draft status And the following data is set with valid values: title, description, times, visibility, maximum guests When creator readies the event Then the event is made ready
    [Fact]
    public void GivenEventExistWithIdAndIsDraftStatus_WhenCreatorReadiesTheEvent_ThenEventIsMadeReady()
    {
        // Arrange
        var veaEvent = EventFactory.Create()
            .WithTitle("Title")
            .WithDescription("Description")
            .WithStatus(EventStatus.Draft)
            .WithGuestLimit(25)
            .WithVisibility(EventVisibility.Private)
            .Build();
        // Act
        veaEvent.Ready();

        // Assert
        Assert.Equal(EventStatus.Ready, veaEvent.Status);
    }

    //F1Given an existing event with ID And the event is in draft status And the any of the following is true: · Title is not set or remains default value · Description is not set or remains default value · Times are not set or remains default value · visibility is not set · maximum guests is not between 5 and 50 When creator readies the event Then a failure message is provided explaining what data is missing
    [Fact]
    public void GivenEventExistWithIdAndIsDraftStatusAndAnyOfTheFollowingIsTrue_WhenCreatorReadiesTheEvent_ThenFailureMessageIsProvided()
    {
        // Arrange
        var veaEvent = EventFactory.Create()
            .WithTitle("Working Title")
            .WithDescription("")
            .WithStatus(EventStatus.Draft)
            .WithGuestLimit(4)
            .WithVisibility(EventVisibility.Private)
            .Build();

        // Act
        Result result = veaEvent.Ready();

        // Assert
        Assert.Contains(EventErrors.EventMustHaveValidTitle(), result.Errors);
        Assert.Contains(EventErrors.Description.DescriptionCannotBeEmpty(), result.Errors);
    }
    
    //F2Given an existing event with ID And the event is in cancelled status When creator readies the event Then a failure message is provided explaining a cancelled event cannot be readied
    
    [Fact]
    public void GivenEventExistWithIdAndIsCancelledStatus_WhenCreatorReadiesTheEvent_ThenFailureMessageIsProvided()
    {
        // Arrange
        var veaEvent = EventFactory.Create()
            .WithTitle("Title")
            .WithDescription("Description")
            .WithStatus(EventStatus.Cancelled)
            .WithGuestLimit(25)
            .WithVisibility(EventVisibility.Private)
            .Build();
        
        // Act
        Result result = veaEvent.Ready();

        // Assert
        Assert.Contains(EventErrors.EventMustBeDraft(), result.Errors);
    }
    
    //F3 Given an existing event with ID
    //And the event has a start date/time which is prior to the time of readying
    //When the creator readies the event
    //Then a failure message is provided explaining an event in the past cannot be made ready
    
    [Fact]
    public void GivenEventExistWithIdAndStartDateIsPriorToTimeOfReadying_WhenCreatorReadiesTheEvent_ThenFailureMessageIsProvided()
    {
        // Arrange
        DateRange dateRange = new DateRange(DateTime.Now.AddHours(-1), DateTime.Now.AddHours(1));
        var veaEvent = EventFactory.Create()
            .WithTitle("Title")
            .WithDescription("Description")
            .WithStatus(EventStatus.Draft)
            .WithGuestLimit(25)
            .WithVisibility(EventVisibility.Private)
            .WithDateRange(dateRange)
            .Build();
        // Act
        Result result = veaEvent.Ready();
        
        // Assert
        Assert.Contains(EventErrors.DateRange.EventStartTimeCannotBeInPast(), result.Errors);
    }
    
    
    

    // F4 Given an existing event with ID
    // And the title of the event is the default (see UC1)
    // When creator readies the event
    // Then a failure message is provided explaining the title must changed from the default
    
    [Fact]
    public void GivenEventExistWithIdAndTitleIsDefault_WhenCreatorReadiesTheEvent_ThenFailureMessageIsProvided()
    {
        // Arrange
        var veaEvent = EventFactory.Create()
            .WithTitle("Working Title")
            .WithDescription("Description")
            .WithStatus(EventStatus.Draft)
            .WithGuestLimit(25)
            .WithVisibility(EventVisibility.Private)
            .Build();

        // Act
        Result result = veaEvent.Ready();

        // Assert
        Assert.Contains(EventErrors.EventMustHaveValidTitle(), result.Errors);
    }

}
