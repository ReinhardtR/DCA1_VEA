using VEA.Core.Domain.Aggregates.Events;

namespace VEA.Tests.UnitTests.Features.Event.Create;

public class CreateAggregateTests
{
    // S1
    [Fact]
    public void GivenId_WhenCreatingEvent_ThenCreateEventWithIdAndStatusDraftAndDefaultGuestLimit()
    {  
        // Arrange
        var eventId = EventId.New().Payload;
        
        // Act
        var veaEvent = VeaEvent.Create(eventId);
        
        // Assert
        Assert.Equal(eventId, veaEvent.Id);
        Assert.Equal(EventStatus.Draft, veaEvent.Status);
        Assert.Equal(5, veaEvent.GuestLimit.Value);
    }

    // S2
    [Fact]
    public void GivenId_WhenCreatingEvent_ThenCreateEventWithDefaultTitle()
    {
        // Arrange
        var eventId = EventId.New().Payload;
        
        // Act
        var veaEvent = VeaEvent.Create(eventId);
        
        // Assert
        Assert.Equal("Working Title", veaEvent.Title.Value);
    }
    
    // S3
    [Fact]
    public void GivenId_WhenCreatingEvent_ThenCreateEventWithDefaultDescription()
    {
        // Arrange
        var eventId = EventId.New().Payload;
        
        // Act
        var veaEvent = VeaEvent.Create(eventId);
        
        // Assert
        Assert.Equal("", veaEvent.Description.Value);
    }
    
    // S4
    [Fact]
    public void GivenId_WhenCreatingEvent_ThenCreateEventWithPrivateVisibility()
    {
        // Arrange
        var eventId = EventId.New().Payload;
        
        // Act
        var veaEvent = VeaEvent.Create(eventId);
        
        // Assert
        Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
    }
}