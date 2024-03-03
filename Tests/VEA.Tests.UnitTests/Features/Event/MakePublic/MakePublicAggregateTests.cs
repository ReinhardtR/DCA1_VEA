using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;

namespace VEA.Tests.UnitTests.Features.Event.MakePublic;

public class MakePublicAggregateTests
{
  // S1
  [Fact]
  public void GivenExistingEventIdAndNotCancelled_WhenCreatorMakesEventPublic_ThenMakeEventPublicAndKeepStatus()
  {
    var veaEvent = EventFactory.Create().Build();

    EventStatus prevStatus = veaEvent.Status;

    var result = veaEvent.SetVisibility(EventVisibility.Public);

    Assert.False(result.IsFailure);
    Assert.Equal(prevStatus, veaEvent.Status);
    Assert.Equal(EventVisibility.Public, veaEvent.Visibility);
  }

  // F1
  [Fact]
  public void GivenExistingEventIdAndCancelled_WhenCreatorMakesEventPublic_ThenFail()
  {
    var veaEvent = EventFactory.Create()
      .WithStatus(EventStatus.Cancelled)
      .Build();

    veaEvent.SetVisibility(EventVisibility.Public);

    var result = veaEvent.SetVisibility(EventVisibility.Public);

    Assert.True(result.IsFailure);
    Assert.Equal(EventStatus.Cancelled, veaEvent.Status);
  }
}
