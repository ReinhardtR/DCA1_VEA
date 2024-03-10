using VEA.Core.Domain.Aggregates.Events;
using VEA.Tests.UnitTests.Features.Guest;

namespace VEA.Tests.UnitTests.Features.Event.Participate;

public class ParticipateAggregateTests
{
    // Success 1
    [Fact]
    public void GivenValidData_WhenParticipatingEvent_ShouldSuccess()
    {
        //Arrange  
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .WithGuestLimit(5)
            .Build();
        //Create a guest
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        //Act 
        var result = veaEvent.Participate(guest);
        
        
        //Assert
        Assert.False(result.IsFailure);
    }
    
    // Failure 1
    [Fact] //Draft status
    public void GivenEventStatusIsDraft_WhenParticipatingEvent_ShouldFailure()
    {
        EventStatus eventStatus = EventStatus.Draft;
    }
    
    [Fact] //ready status
    public void GivenEventStatusIsReady_WhenParticipatingEvent_ShouldFailure()
    {
        EventStatus eventStatus = EventStatus.Ready;
    }
    
    [Fact] //cancelled status
    public void GivenEventStatusIsCancelled_WhenParticipatingEvent_ShouldFailure()
    {
        EventStatus eventStatus = EventStatus.Cancelled;
    }
    
    // Failure 2
    [Fact]
    public void GivenGuestLimitReached_WhenParticipatingEvent_ShouldFailure()
    {
        EventGuestLimit eventGuestLimit = EventGuestLimit.Create(5).Payload;
    }
    
    
    // Failure 3
    [Fact]
    public void GivenStartTimeIsBeforeNow_WhenParticipatingEvent_ShouldFailure()
    {
        
    }
    
    // Failure 4
    [Fact]
    public void GivenPrivateEvent_WhenParticipatingEvent_ShouldFailure()
    {
        EventVisibility eventVisibility = EventVisibility.Private;
    }
    
    // Failure 5
    [Fact]
    public void GivenGuestAlreadyParticipated_WhenParticipatingEvent_ShouldFailure()
    {
        
    }
}