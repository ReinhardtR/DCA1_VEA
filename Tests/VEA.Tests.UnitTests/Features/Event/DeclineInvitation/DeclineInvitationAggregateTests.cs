using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Tests.UnitTests.Features.Event.DeclineInvitation;

public class DeclineInvitationAggregateTests
{
    // S1
    [Fact]
    public void GivenActiveEventAndRegisteredGuestAndGuestHasPendingInvitation_WhenGuestDeclinesInvitation_ThenSuccess()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        var invitation = Invitation.Create(InvitationId.New().Payload, InvitationStatus.Pending, guest);
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithInvitations([invitation])
            .Build();

        //Act
        var result = veaEvent.DeclineInvitation(guest);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(InvitationStatus.Declined, invitation.InvitationStatus);
    }
    
    // S2
    [Fact]
    public void GivenActiveEventAndRegisteredGuestAndGuestHasAcceptedInvitation_WhenGuestDeclinesInvitation_ThenSuccess()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        var invitation = Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, guest);
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithInvitations([invitation])
            .Build();

        //Act
        var result = veaEvent.DeclineInvitation(guest);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(InvitationStatus.Declined, invitation.InvitationStatus);
    }
    
    // F1
    [Fact]
    public void GivenExistingEventAndRegisteredGuestAndGuestHasNoInvitation_WhenGuestDeclinesInvitation_ThenFailure()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithInvitations([])
            .Build();

        //Act
        var result = veaEvent.DeclineInvitation(guest);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(Invitation.Errors.GuestHasNoInvitation(), result.Errors);
    }
    
    // F2
    [Fact]
    public void GivenCancelledEventAndRegisteredGuestAndGuestHasPendingInvitation_WhenGuestDeclinesInvitation_ThenFailure()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        var invitation = Invitation.Create(InvitationId.New().Payload, InvitationStatus.Pending, guest);
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Cancelled)
            .WithInvitations([invitation])
            .Build();

        //Act
        var result = veaEvent.DeclineInvitation(guest);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(Invitation.Errors.EventIsCancelled(), result.Errors);
    }
    
    // F3
    [Fact]
    public void GivenReadyEventAndRegisteredGuestAndGuestHasPendingInvitation_WhenGuestDeclinesInvitation_ThenFailure()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        var invitation = Invitation.Create(InvitationId.New().Payload, InvitationStatus.Pending, guest);
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
            .WithInvitations([invitation])
            .Build();

        //Act
        var result = veaEvent.DeclineInvitation(guest);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(Invitation.Errors.EventIsNotActive(), result.Errors);
    }
}