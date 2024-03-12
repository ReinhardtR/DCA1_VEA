using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;

namespace VEA.Tests.UnitTests.Features.Event.MakePrivate;

public class MakePrivateAggregateTests
{
    // S1 - Draft
    [Fact]
    public void GivenExistingEventIdAndPrivateAndDraft_WhenCreatorMakesEventPrivate_ThenSucceedWithNoChange_Draft()
    {
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Draft)
            .WithVisibility(EventVisibility.Private)
            .Build();

        var result = veaEvent.SetVisibility(EventVisibility.Private);

        Assert.False(result.IsFailure);
        Assert.Equal(EventStatus.Draft, veaEvent.Status);
        Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
    }

    // S1 - Ready
    [Fact]
    public void GivenExistingEventIdAndPrivateAndReady_WhenCreatorMakesEventPrivate_ThenSucceedWithNoChange_Ready()
    {
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
            .WithVisibility(EventVisibility.Private)
            .Build();

        var result = veaEvent.SetVisibility(EventVisibility.Private);

        Assert.False(result.IsFailure);
        Assert.Equal(EventStatus.Ready, veaEvent.Status);
        Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
    }

    // S2 - Draft
    [Fact]
    public void GivenExistingEventIdAndPublicAndDraft_WhenCreatorMakesEventPrivate_ThenEventPrivateAndDraft_Draft()
    {
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Draft)
            .WithVisibility(EventVisibility.Public)
            .Build();

        var result = veaEvent.SetVisibility(EventVisibility.Private);

        Assert.False(result.IsFailure);
        Assert.Equal(EventStatus.Draft, veaEvent.Status);
        Assert.Equal(EventVisibility.Private, veaEvent.Visibility);
    }

    // S2 - Ready
    [Fact]
    public void GivenExistingEventIdAndPublicAndDraft_WhenCreatorMakesEventPrivate_ThenEventPrivateAndDraft_Ready()
    {
        var veaEvent = EventFactory.Create()
            .WithStatus(EventStatus.Ready)
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
