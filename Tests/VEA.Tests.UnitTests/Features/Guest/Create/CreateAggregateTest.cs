using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Guest.Create;


public class CreateAggregateTest
{
  // Success 1
  
  // Failure 1
  // Wrong email is without "@via.dk"
  [Theory]
  [InlineData("wrongemail.com")]
  [InlineData("AnotherWrong@gmail.com")]
  [InlineData("Wrongvia.dk")]
  public void GivenWrongEmail_WhenCreatingAccount_ShouldFailure(string wrongEmail)
  {
    // Arrange
    Result<ViaEmail> result;
    // Act
    result = ViaEmail.Create(wrongEmail);
    // Assert;
    Assert.True(result.IsFailure);
    Assert.Contains(ViaEmail.Errors.EmailDomainNotValid(), result.Errors);
  }
  
  // Failure 2
  
  // Failure 3
  
  // Failure 4
  
  // Failure 5
  
  // Failure 6
  
  // Failure 7
  
  
}