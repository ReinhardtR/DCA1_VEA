using System.Diagnostics;
using VEA.Core.Domain.Aggregates.Events;
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
        //Arrange  
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithVisibility(EventVisibility.Public)
            .WithGuestLimit(5)
            .Build();
        //Create a guest
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        //Create a participation
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        //Act 
        var result = veaEvent.Participate(participation);
        
        
        //Assert
        Assert.False(result.IsFailure);
    }
    
    // Failure 1
    [Fact] //Draft status
    public void GivenEventStatusIsDraft_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        EventStatus eventStatus = EventStatus.Draft;
        
       
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
       
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    [Fact] //ready status
    public void GivenEventStatusIsReady_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        EventStatus eventStatus = EventStatus.Ready;
        
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    [Fact] //cancelled status
    public void GivenEventStatusIsCancelled_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        EventStatus eventStatus = EventStatus.Cancelled;
        
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(eventStatus)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventIsNotActive(), result.Errors);
    }
    
    // Failure 2
    [Fact]
    public void GivenGuestLimitReached_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithGuestLimit(1)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest1 = GuestFactory.Create().Build();
        Core.Domain.Aggregates.Guests.Guest guest2 = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest1.Id,
            null // ParticipationStatus.Participating
        );
        
        Participation participation2 = Participation.Create(
            ParticipationId.New(),
            guest2.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        var result2 = veaEvent.Participate(participation2);
        
        //Assert
        Assert.True(result2.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.GuestLimitReached(), result2.Errors);
    }
    
    
    // Failure 3
    [Fact]
    public void GivenStartTimeIsBeforeNow_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        DateTime startTime = DateTime.Now.AddDays(-1);
        DateRange dateRange = new DateRange(startTime, DateTime.Now.AddDays(1));

        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithDateRange(dateRange)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventAlreadyStarted(), result.Errors);
    }
    
    // Failure 4
    [Fact]
    public void GivenPrivateEvent_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        EventVisibility eventVisibility = EventVisibility.Private;
        
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithVisibility(eventVisibility)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventNotPublic(), result.Errors);
    }
    
    // Failure 5
    [Fact]
    public void GivenGuestAlreadyParticipated_WhenParticipatingEvent_ShouldFailure()
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .Build();
        
        Core.Domain.Aggregates.Guests.Guest guest = GuestFactory.Create().Build();
        
        Participation participation = Participation.Create(
            ParticipationId.New(),
            guest.Id,
            null // ParticipationStatus.Participating
        );
        
        veaEvent.Participate(participation);
        
        //Act
        var result = veaEvent.Participate(participation);
        
        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.GuestAlreadyParticipated(), result.Errors);
    }
}