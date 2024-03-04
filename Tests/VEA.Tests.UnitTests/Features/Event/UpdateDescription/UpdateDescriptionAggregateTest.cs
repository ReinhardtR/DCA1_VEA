using VEA.Core.Domain;
using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Event.UpdateDescription;

public class UpdateDescriptionAggregateTest
{
    //UseCase 3
    
    
    
    //Success 1
    
    
    //Success 2
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void GivenEmptyString_WhenUpdatingDescription_ThenSuccess(string input)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        EventDescription description = EventDescription.Create(input).Payload;

        //Act
        Result result = veaEvent.UpdateDescription(description);

        //Assert
        Assert.False(result.IsFailure);
        Assert.True(result.Errors.Count == 0);
    }
    
    //Success 3
    [Theory]
    [InlineData("This is a completely valid description")]
    public void GivenValidString_WhenUpdatingDescription_ThenEventStatusIsSetToDraft(string input)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        EventDescription description = EventDescription.Create(input).Payload;

        //Act
        Result result = veaEvent.UpdateDescription(description);

        //Assert
        Assert.False(result.IsFailure);
        // Assert that event status it now draft
        Assert.True(result.Errors.Count == 0);
    }
    
    // Failure 1
    [Theory]
    //Inlinedata with more than 250 characters
    [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam auctor, nisi non vestibulum laoreet, nulla tortor interdum orci, id bibendum ligula nulla sed odio. Aliquam nec est ut justo finibus gravida a eget tortor. Phasellus nec turpis malesuada, vulputate nulla eu, dapibus mauris. Vivamus sit amet tortor sit amet dui dictum sodales. Mauris semper vulputate tellus, ac pharetra enim fermentum eu. Fusce nec libero pretium, mattis nisi nec, posuere velit. Proin tincidunt vel ligula id condimentum. Vestibulum dapibus justo sed malesuada bibendum. Sed ut tincidunt justo, sed facilisis purus. Suspendisse potenti. Curabitur elementum ut sem sit amet sollicitudin. In vel tincidunt orci. Fusce id nisi non mi tincidunt auctor non nec velit. Phasellus rutrum elit nec nisi mattis placerat. Nulla facilisi.")]
    public void GivenTooLongString_WhenUpdatingDescription_ThenFailure(string input)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().Build();
        EventDescription description = EventDescription.Create(input).Payload;

        //Act
        Result result = veaEvent.UpdateDescription(description);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.DescriptionCannotBeLongerThan250Characters(), result.Errors);
    }

    /*
    // Failure 2
    [Theory]
    [InlineData("This is a completely valid description")]
    public void GivenValidString_WhenEventStatusIsCancelled_ThenFailure(string input)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().WithStatus(EventStatus.Cancelled).Build();
        EventDescription description = EventDescription.Create(input).Payload;

        //Act
        Result result = veaEvent.UpdateDescription(description);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.CannotUpdateCancelledEvent(), result.Errors);
    }
    
    // Failure 3
    [Theory]
    [InlineData("This is a completely valid description")]
    public void GivenValidString_WhenEventStatusIsActive_ThenFailure(string input)
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create().WithStatus(EventStatus.Started).Build();
        EventDescription description = EventDescription.Create(input).Payload;

        //Act
        Result result = veaEvent.UpdateDescription(description);

        //Assert
        Assert.True(result.IsFailure);
        Assert.Contains(EventErrors.CannotUpdateStartedEvent(), result.Errors);
    }
    */
}