using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Domain.Common.Values;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.Activate;

public class ActivateEventAggregateTest
{
    public class S1
    {
        // S1
        // Given an existing event with ID
        // And the event is in draft status
        // And the following data is set with valid values: title, description, times, visibility, maximum guests
        // When creator activates the event
        // Then the event is first made ready, and if successful, then made active
        
        [Fact]
        public void GivenEventExistWithIdAndIsDraftStatusAndDataIsSetWithValidValues_WhenCreatorActivatesTheEvent_ThenEventIsFirstMadeReadyAndIfSuccessfulThenMadeActive()
        {
            // Arrange
            var veaEvent = EventFactory.Create().WithTitle("Title").WithDescription("Description")
                .WithDateRange(new DateRange(DateTime.Today.AddDays(1).AddHours(10), DateTime.Today.AddDays(1).AddHours(15)))
                .Build();
            
            // Act
            Result result = veaEvent.Activate();
            
            // Assert
            Assert.False(result.IsFailure);
            Assert.Equal(EventStatus.Active, veaEvent.Status);
        }
        
    }
    
    public class S2
    {
        // S2
        // Given an existing event with ID
        // And the event is in ready status
        // When creator activates the event
        // Then the event is then active
        
        [Fact]
        public void GivenEventExistWithIdAndIsReadyStatus_WhenCreatorActivatesTheEvent_ThenEventIsMadeActive()
        {
            // Arrange
            var veaEvent = EventFactory.Create().WithStatus(EventStatus.Ready).Build();
            
            // Act
            veaEvent.Activate();
            
            // Assert
            Assert.Equal(EventStatus.Active, veaEvent.Status);
        }
    }

    public class S3
    {
        // S3
        // Given an existing event with ID
        // And the event is in active status
        // When creator activates the event
        // Then nothing changes, the event successfully remains active
        
        [Fact]
        public void GivenEventExistWithIdAndIsActiveStatus_WhenCreatorActivatesTheEvent_ThenEventSuccessfullyRemainsActive()
        {
            // Arrange
            var veaEvent = EventFactory.Create().WithStatus(EventStatus.Active).Build();
            
            // Act
            veaEvent.Activate();
            
            // Assert
            Assert.Equal(EventStatus.Active, veaEvent.Status);
        }

    }

    public class F1
    {
        // F1
        // Given an existing event with ID
        // And the event is in draft status
        // And any of the following data is not set with valid values: title, description, times, visibility, maximum number of guests (i.e. ready-able, see UC8)
        // When creator activates the event
        // Then a failure message is provided explaining what data is missing
        
        [Fact]
        public void GivenEventExistWithIdAndIsDraftStatusAndAnyOfTheFollowingDataIsNotSetWithValidValues_WhenCreatorActivatesTheEvent_ThenFailureMessageIsProvided()
        {
            // Arrange
            var veaEvent = EventFactory.Create()
                .WithStatus(EventStatus.Draft)
                .WithTitle("Working Title")
                .WithDescription("")
                .WithGuestLimit(3)
                .Build();
            
            // Act
            Result result = veaEvent.Activate();
            
            // Assert
            Assert.Contains(VeaEvent.Errors.Event.EventMustHaveValidTitle(), result.Errors);
            Assert.Contains(EventDescription.Errors.DescriptionCannotBeEmpty(), result.Errors);
        }

    }

    public class F2
    {
        // F2
        // Given an existing event with ID
        // And the event is in cancelled status
        // When creator activates the event
        // Then a failure message is provided explaining a cancelled event cannot be activated
        
        [Fact]
        public void GivenEventExistWithIdAndIsCancelledStatus_WhenCreatorActivatesTheEvent_ThenFailureMessageIsProvided()
        {
            // Arrange
            var veaEvent = EventFactory.Create().WithStatus(EventStatus.Cancelled).Build();
            
            // Act
            Result result = veaEvent.Activate();
            
            // Assert
            Assert.Contains(VeaEvent.Errors.Event.EventCannotBeActivatedWhenCancelled(), result.Errors);
        }

    }
}