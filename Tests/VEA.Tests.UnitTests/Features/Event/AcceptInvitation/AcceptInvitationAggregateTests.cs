using System.ComponentModel;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;

namespace VEA.Tests.UnitTests.Features.Event.AcceptInvitation;

public class AcceptInvitationAggregateTests
{
    // S1
    [Fact]
    public void GivenActiveEventAndRegisteredGuestAndGuestHasPendingInvitationAndParticipatingGuestsIsLessThanMaximum_WhenGuestAcceptsInvitation_ThenSuccess()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        List<Invitation> invitations = [Invitation.Create(InvitationId.New().Payload, null, guest)];
        var veaEvent = EventFactory.Create()
            .WithGuestLimit(10)
            .WithStatus(EventStatus.Active)
            .WithInvitations(invitations)
            .Build();

        //Act
        var result = veaEvent.AcceptInvitation(guest);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(InvitationStatus.Accepted, veaEvent.Invitations[0].InvitationStatus);
    }
    
    //F1
    [Fact]
    public void GivenExistingEventAndRegisteredGuestAndGuestHasNoPendingInvitation_WhenGuestAcceptsInvitation_ThenFailure()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        List<Invitation> invitations = [];
        var veaEvent = EventFactory.Create()
            .WithGuestLimit(10)
            .WithStatus(EventStatus.Active)
            .WithInvitations(invitations)
            .Build();

        //Act
        var result = veaEvent.AcceptInvitation(guest);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Invitation.GuestHasNoInvitation(), result.Errors);
    }
    
    //F2
    [Fact]
    public void GivenExistingEventAndRegisteredGuestAndGuestHasPendingInvitationAndParticipatingGuestsIsHigherThanMaximum_WhenGuestAcceptsInvitation_ThenFailure()
    {
        //Arrange
        var guest = GuestId.New().Payload;
        List<Invitation> invitations = [Invitation.Create(InvitationId.New().Payload, null, guest), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload)];
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Active)
            .WithGuestLimit(5)
            .WithInvitations(invitations)
            .Build();

        //Act
        var result = veaEvent.AcceptInvitation(guest);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(VeaEvent.Errors.Invitation.GuestLimitReached(), result.Errors);
    }
}