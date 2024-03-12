using System.Diagnostics;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;
using VEA.Tests.UnitTests.Features.Guest;
using Xunit.Abstractions;

namespace VEA.Tests.UnitTests.Features.Event.Participate;

public class ParticipateAggregateTests
{
    // Success 1
    [Fact]
    public void GivenValidData_WhenParticipatingEvent_ShouldSuccess()
    {
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .WithGuestLimit(5)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act 
        var result = veaEvent.Participate(guest.Id);
        
        
        //Assert
        Assert.False(result.IsFailure);
    }
    
    // Failure 1
    [Fact] //Draft status
    public void GivenEventStatusIsDraft_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var eventStatus = EventStatus.Draft;
        
        var veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    [Fact] //ready status
    public void GivenEventStatusIsReady_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var eventStatus = EventStatus.Ready;
        
        var veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    [Fact] //cancelled status
    public void GivenEventStatusIsCancelled_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var eventStatus = EventStatus.Cancelled;
        
        var veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    // Failure 2
    [Fact]
    public void GivenGuestLimitReached_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        const int guestLimit = 10;
        
        List<GuestId> guestIds = Enumerable.Range(0, guestLimit)
            .Select(_ => GuestFactory.Create().Build().Id)
            .ToList();
        
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithParticipants(guestIds)
            .WithGuestLimit(guestLimit)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.GuestLimitReached(), result.Errors);
    }
    
    
    // Failure 3
    [Fact]
    public void GivenStartTimeIsBeforeNow_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var dateRange = new DateRange(
            DateTime.Today.AddDays(-1).AddHours(9),
            DateTime.Today.AddDays(-1).AddHours(10)
        );
        
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithDateRange(dateRange)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventAlreadyStarted(), result.Errors);
    }
    
    // Failure 4
    [Fact]
    public void GivenPrivateEvent_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var eventVisibility = EventVisibility.Private;
        
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithVisibility(eventVisibility)
            .Build();
        
        var guest = GuestFactory.Create().Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventNotPublic(), result.Errors);
    }
    
    // Failure 5
    [Fact]
    public void GivenGuestAlreadyParticipated_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        var guest = GuestFactory.Create().Build();
        
        var veaEvent = EventFactory.Create()
            .WithParticipants([guest.Id])
            .WithStatus(EventStatus.Active)
            .Build();
        
        //Act
        var result = veaEvent.Participate(guest.Id);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.GuestAlreadyParticipated(), result.Errors);
    }
}