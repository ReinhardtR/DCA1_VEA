using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.UpdateTitle;

public class UpdateTitleAggregateTest
{
    //S1
    //TODO - depends on UC 8
    [Fact]
    public void EventExistWithIdIsDraftValidTitle_UpdatesTitle_TitleIsUpdated()
    {
        // Arrange
        EventFactory eventFactory = EventFactory.Create();
        EventTitle title = EventTitle.Create("title").Payload;
        VeaEvent veaEvent = eventFactory.WithId(new Guid()).Build();
        
        // Act
        veaEvent.UpdateTitle(title);

        // Assert
        Assert.Equal(title, veaEvent.Title);
    }
    
    //S2
    //S2 Given an existing event with ID When creator selects to set the title of the event | Scary Movie Night! | | Graduation Gala | | VIA Hackathon | And the title length is between 3 and 75 (inclusive) characters And the event is in ready status Then the title of the event is updated And the event is in draft status
    [Theory]
    [InlineData("Scary Movie Night!")]
    [InlineData("Graduation Gala")]
    [InlineData("VIA Hackathon")]
    public void GivenEventExistWithIdAndIsReadyStatusAndTitleLengthIsBetween3And75Characters_WhenUpdatingTitle_ThenTitleIsUpdated(string input)
    {
        // Arrange
        EventFactory eventFactory = EventFactory.Create();
        EventTitle title = EventTitle.Create(input).Payload;
        VeaEvent veaEvent = eventFactory.WithId(new Guid()).Build();
        
        // Act
        veaEvent.UpdateTitle(title);

        // Assert
        Assert.Equal(title, veaEvent.Title);
    }
    
    //F1 Given an existing event with ID When creator selects to set the title of the event And the title is 0 characters Then a failure message is returned explaining that the title must be between 3 and 75 characters
    [Fact]
    public void GivenEventExistWithIdAndTitleIs0Characters_WhenUpdatingTitle_ThenFailureMessageIsReturned()
    {
        // Arrange
        // Act
        Result<EventTitle> result = EventTitle.Create("");
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.TitleMustBeBetween3And75Characters(), result.Errors);
    }
    
    //F2 Given an existing event with ID When creator selects to set the title of the event | XY | | a | And the title is less than 3 characters Then a failure message is returned explaining that the title must be between 3 and 75 characters
    [Theory]
    [InlineData("XY")]
    [InlineData("a")]
    public void GivenEventExistWithIdAndTitleIsLessThan3Characters_WhenUpdatingTitle_ThenFailureMessageIsReturned(string input)
    {
        // Arrange
        // Act
        Result<EventTitle> result = EventTitle.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.TitleMustBeBetween3And75Characters(), result.Errors);
    }
    
    //F3 Given an existing event with ID When creator selects to set the title of the event And the title is more than 75 characters Then a failure message is returned explaining that the title must be between 3 and 75 characters
    
    [Theory]
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam auctor, nisi non vestibulum laoreet, nulla tortor interdum orci, id bibendum ligula nulla sed odio. Aliquam nec est ut justo finibus gravida a eget tortor. Phasellus nec turpis malesuada, vulputate nulla eu, dapibus mauris. Vivamus sit amet tortor sit amet dui dictum sodales. Mauris semper vulputate tellus, ac pharetra enim fermentum eu. Fusce nec libero pretium, mattis nisi nec, posuere velit. Proin tincidunt vel ligula id condimentum. Vestibulum dapibus justo sed malesuada bibendum. Sed ut tincidunt justo, sed facilisis purus. Suspendisse potenti. Curabitur elementum ut sem sit amet sollicitudin. In vel tincidunt orci. Fusce id nisi non mi tincidunt auctor non nec velit. Phasellus rutrum elit nec nisi mattis placerat. Nulla facilisi.")]
    public void GivenEventExistWithIdAndTitleIsMoreThan75Characters_WhenUpdatingTitle_ThenFailureMessageIsReturned(string input)
    {
        // Arrange
        // Act
        Result<EventTitle> result = EventTitle.Create(input);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.TitleMustBeBetween3And75Characters(), result.Errors);
    }
    
    //F4 Given an existing event with ID When creator selects to set the title of the event And the title is non-existing (null) Then a failure message is returned explaining that the title must be between 3 and 75 characters
    [Fact]
    public void GivenEventExistWithIdAndTitleIsNonExisting_WhenUpdatingTitle_ThenFailureMessageIsReturned()
    {
        // Arrange
        // Act
        Result<EventTitle> result = EventTitle.Create(null);
        
        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.TitleMustBeBetween3And75Characters(), result.Errors);
    }
    
    //F5 Given an existing event with ID When creator selects to set the title of the event And the event is in active status Then a failure message is returned explaining an active event cannot be modified
    // Todo - depends on UC 8
    [Fact]
    public void GivenEventExistWithIdAndIsActiveStatus_WhenUpdatingTitle_ThenFailureMessageIsReturned()
    {
        // Arrange
        EventFactory eventFactory = EventFactory.Create();
        EventTitle title = EventTitle.Create("title").Payload;
        VeaEvent veaEvent = eventFactory.WithId(new Guid()).Build();
        //veaEvent.ReadyToStart();
        
        // Act
        Result result = veaEvent.UpdateTitle(title);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.UpdateTitleWhenEventActive(), result.Errors);
    }
    
    //F6 Given an existing event with ID When creator selects to set the title of the event And the event is in cancelled status Then a failure message is returned explaining a cancelled event cannot be modified
    // Todo - depends on UC 8
    [Fact]
    public void GivenEventExistWithIdAndIsCancelledStatus_WhenUpdatingTitle_ThenFailureMessageIsReturned()
    {
        // Arrange
        EventFactory eventFactory = EventFactory.Create();
        EventTitle title = EventTitle.Create("title").Payload;
        VeaEvent veaEvent = eventFactory.WithId(new Guid()).Build();
        //veaEvent.Cancel();
        
        // Act
        Result result = veaEvent.UpdateTitle(title);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.UpdateTitleWhenEventCancelled(), result.Errors);
    }
}