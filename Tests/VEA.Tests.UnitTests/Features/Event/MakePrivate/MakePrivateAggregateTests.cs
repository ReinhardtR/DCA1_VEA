using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;

namespace VEA.Tests.UnitTests.Features.Event.MakePrivate;

public class MakePrivateAggregateTests
{
  // S1
  [Theory]
  [InlineData(EventStatus.Draft)]
  [InlineData(EventStatus.Ready)]
  public void GivenExistingEventIdAndPrivateAndDraftOrReady_WhenCreatorMakesEventPrivate_ThenSucceedWithNoChange(EventStatus status)
  {
    var veaEvent = EventFactory.Create()
      .WithStatus(status)
      .WithVisibility(EventVisibility.Private)
      .Build();

    var result = veaEvent.SetVisibility(EventVisibility.Private);

    Assert.False(result.IsFailure);
    Assert.Equal(status, veaEvent.Status);
    Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
  }

  // S2
  [Theory]
  [InlineData(EventStatus.Draft)]
  [InlineData(EventStatus.Ready)]
  public void GivenExistingEventIdAndPublicAndDraft_WhenCreatorMakesEventPrivate_ThenEventPrivateAndDraft(EventStatus status)
  {
    var veaEvent = EventFactory.Create()
      .WithStatus(status)
      .WithVisibility(EventVisibility.Public)
      .Build();

    var result = veaEvent.SetVisibility(EventVisibility.Private);

    Assert.False(result.IsFailure);
    Assert.Equal(EventStatus.Draft, veaEvent.Status);
    Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
  }

  // F1
  [Fact]
  public void GivenExistingEventIdAndActive_WhenCreatorMakesEventPrivate_ThenFail()
  {
    var veaEvent = EventFactory.Create()
      .WithStatus(EventStatus.Active)
      .Build();

    var result = veaEvent.SetVisibility(EventVisibility.Private);

    Assert.True(result.IsFailure);
  }

  // F2
  [Fact]
  public void GivenExistingEventIdAndCancelled_WhenCreatorMakesEventPrivate_ThenFail()
  {
    var veaEvent = EventFactory.Create()
      .WithStatus(EventStatus.Cancelled)
      .Build();

    var result = veaEvent.SetVisibility(EventVisibility.Private);

    Assert.True(result.IsFailure);
  }
}
