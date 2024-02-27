using VEA.Core.Domain;

namespace VEA.Tests.UnitTests.UpdateDescription;

public class UpdateDescriptionAggregateTest
{
    //UseCase 3
    
    
    //Success 1
    [Fact]
    public void GivenTooLongDescription_WhenUpdatingDescription_ThenFailure()
    {
        //Arrange
        VeaEvent veaEvent = EventFactory.Create()
            .WithId()
        
        //Act 
        
        //Assert
    }
}