using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.ExtendInvitation;

public class ExtendInvitationAggregateTests
{
    // S1
    [Fact]
    public void GivenExistingEventIdAndReadyOrActiveAndRegisteredGuest_WhenCreatorInvitesGuest_ThenSuccess()
    {
        //Arrange
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
            .Build();
        var invitation = Invitation.Create(InvitationId.New().Payload, null, GuestId.New().Payload);

        //Act
        var result = veaEvent.ExtendInvitation(invitation);

        //Assert
        Assert.False(result.IsFailure);
        Assert.Equal(invitation, veaEvent.Invitations[0]);
    }
    
    // F1
    [Fact]
    public void GivenExistingEventIdAndDraftOrCancelledAndRegisteredGuest_WhenCreatorInvitesGuest_ThenFailure()
    {
        //Arrange
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Draft)
            .Build();
        var invitation = Invitation.Create(InvitationId.New().Payload, null, GuestId.New().Payload);

        //Act
        var result = veaEvent.ExtendInvitation(invitation);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.Invitation.ExtendInvitationWhenEventDraftOrCancelled(), result.Errors);
    }
    
    // F2 - Depends on an implementation of a method that counts number of guests invited versus the guestLimit.
    // Depends on UC 11 and UC 14 which aren't implemented yet
    [Fact]
    public void GivenExistingEventIdAndReadyOrActiveAndRegisteredGuestAndMaximumNumberOfGuestsIsMet_WhenCreatorInvitesGuest_ThenFailure()
    {
        //Arrange
        List<Invitation> invitations = [Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload), Invitation.Create(InvitationId.New().Payload, InvitationStatus.Accepted, GuestId.New().Payload)];
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
            .WithInvitations(invitations)
            .WithGuestLimit(5)
            .Build();
        var invitation = Invitation.Create(InvitationId.New().Payload, null, GuestId.New().Payload);

        //Act
        var result = veaEvent.ExtendInvitation(invitation);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(Invitation.Errors.GuestLimitReached(), result.Errors);
    }
}