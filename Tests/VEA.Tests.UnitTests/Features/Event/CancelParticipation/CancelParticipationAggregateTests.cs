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
        var participation = Participation.Create(ParticipationId.New(), guestId, ParticipationStatus.Participating);
        var veaEvent = EventFactory.Create()
            .WithParticipations([participation])
            .Build();

        // Act
        var result = veaEvent.CancelParticipation(participation);
        
        // Assert
        Assert.False(result.IsFailure);
        Assert.DoesNotContain(participation, veaEvent.Participations);
    }
    
    // S2 - doesn't make sense with current participation implementation
    // [Fact]
    // public void GivenExistingEventAndRegisteredGuestAndGuestNotParticipating_WhenGuestCancelsParticipation_ThenNothingChanges()
    // {
    //     // Arrange
    //     var veaEvent = EventFactory.Create().Build();
    //
    //     // Act
    //     List<Participation> previousParticipations = veaEvent.Participations.ToList();
    //     var result = veaEvent.CancelParticipation(participation);
    //     
    //     // Assert
    //     Assert.True(result.IsFailure);
    //     Assert.Contains(participation, veaEvent.Participations);
    // }
    
    // F1
    [Fact]
    public void
        GivenExistingEventAndGuestRegisteredAndGuestParticipatingAndEventStartTimeIsPast_WhenGuestCancelsParticipation_ThenFail()
    {
        // Arrange
        var guestId = GuestId.New().Payload;
        var participation = Participation.Create(ParticipationId.New(), guestId, ParticipationStatus.Participating);
        
        var dateRange = new DateRange(
            DateTime.Today.AddDays(-1).AddHours(9),
            DateTime.Today.AddDays(-1).AddHours(10)
        );
        
        var veaEvent = EventFactory.Create()
            .WithParticipations([participation])
            .WithDateRange(dateRange)
            .Build();

        // Act
        var result = veaEvent.CancelParticipation(participation);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Participation.EventAlreadyStarted(), result.Errors);
    }
}