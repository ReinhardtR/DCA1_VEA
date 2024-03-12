using System.Globalization;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Domain.Common.Values;

namespace VEA.Tests.UnitTests.Features.Event.CancelParticipation;

public class CancelParticipationAggregateTests
{
    // S1
    [Fact]
    public void GivenExistingEventAndRegisteredGuestAndGuestParticipating_WhenGuestCancelsParticipation_ThenGuestParticipationIsCancelled()
    {
        // Arrange
        var guestId = GuestId.New().Payload;
        var veaEvent = EventFactory.Create()
            .WithParticipants([guestId])
            .Build();

        // Act
        var result = veaEvent.CancelParticipation(guestId);
        
        // Assert
        Assert.False(result.IsFailure);
        Assert.DoesNotContain(guestId, veaEvent.Participants);
    }
    
    // S2 
    [Fact]
    public void GivenExistingEventAndRegisteredGuestAndGuestNotParticipating_WhenGuestCancelsParticipation_ThenNothingChanges()
    {
        // Arrange
        var guestId = GuestId.New().Payload;
        var veaEvent = EventFactory.Create().Build();
    
        // Act
        List<GuestId> previousParticipants = veaEvent.Participants.ToList();
        var result = veaEvent.CancelParticipation(guestId);
        
        // Assert
        Assert.True(!result.IsFailure);
        Assert.Equal(previousParticipants, veaEvent.Participants);
    }
    
    // F1
    [Fact]
    public void
        GivenExistingEventAndGuestRegisteredAndGuestParticipatingAndEventStartTimeIsPast_WhenGuestCancelsParticipation_ThenFail()
    {
        // Arrange
        var guestId = GuestId.New().Payload;
        
        var dateRange = new DateRange(
            DateTime.Today.AddDays(-1).AddHours(9),
            DateTime.Today.AddDays(-1).AddHours(10)
        );
        
        var veaEvent = EventFactory.Create()
            .WithParticipants([guestId])
            .WithDateRange(dateRange)
            .Build();

        // Act
        var result = veaEvent.CancelParticipation(guestId);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventAlreadyStarted(), result.Errors);
    }
}