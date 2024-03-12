using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Features.Guest.Create;


public class CreateAggregateTest
{
  // Success 1
  [Fact]
  public void GivenValidData_WhenCreatingAccount_ShouldSuccess()
  {
    //Create all the Valueobject necessary for the Guest
    FirstName firstName = FirstName.Create("John").Payload;
    LastName lastName = LastName.Create("Doe").Payload;
    ViaEmail email = ViaEmail.Create("John@via.dk").Payload;
    Url profilePicture = Url.Create("https://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50").Payload;
    
    //Create the Guest
    Core.Domain.Aggregates.Guests.Guest guest = Core.Domain.Aggregates.Guests.Guest.Create(
      GuestId.New().Payload,
      firstName,
      lastName,
      email,
      profilePicture
    );
    
    //Assert
    Assert.NotNull(guest);
  }
  
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
  [Theory]
  [InlineData("w@rong@via.dk")]
  [InlineData("wr@ong@via.dk")]
  [InlineData("wrong..@via.dk")]
  [InlineData("@via.dk")]
  [InlineData("tooManyLetters@via.dk")]
  [InlineData("a123@via.dk")]
  [InlineData("123abc@via.dk")]
  public void GivenEmailInWrongFormat_WhenCreatingAccount_ShouldFailure(string wrongFormatEmail)
  {
    // Arrange
    Result<ViaEmail> result;
    // Act
    result = ViaEmail.Create(wrongFormatEmail);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(ViaEmail.Errors.EmailFormatNotValid(), result.Errors);
  }
  
  // Failure 3
  [Theory]
  [InlineData("a")]
  [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZAAAAAAAAAAAA")] //More than 25 characters
  public void GivenInvalidFirstName_WhenCreatingAccount_ShouldFailure(string wrongFirstName)
  {
    // Arrange
    Result<FirstName> result;
    // Act
    result = FirstName.Create(wrongFirstName);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(FirstName.Errors.FirstNameNotValid(), result.Errors);
  }
  
  // Failure 4
  [Theory]
  [InlineData("b")]
  [InlineData("Surelythisiswayyyyytoolongtobeanactuallastnameright")]
  public void GivenInvalidLastName_WhenCreatingAccount_ShouldFailure(string wrongLastName)
  {
    // Arrange
    Result<LastName> result;
    // Act
    result = LastName.Create(wrongLastName);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(LastName.Errors.LastNameNotValid(), result.Errors);
  }
  
  // Failure 5 todo this test depends on Persistence, which we do not yet have
  /*
  [Theory]
  [InlineData("Milo@via.dk")]
  public void GivenEmailAlreadyInUse_WhenCreatingAccount_ShouldFailure(string emailInUse)
  {
    // Arrange
    Result<ViaEmail> result;
    // Act
    result = ViaEmail.Create(emailInUse);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(ViaEmail.Errors.EmailAlreadyInUse(), result.Errors);
  }
  */
  
  // Failure 6
  [Theory]
  [InlineData("M1l0")]
  [InlineData("R31nh4rd")]
  [InlineData("S3b4st14n")]
  [InlineData("J4k0b")]
  public void GivenFirstNameContainingNumbers_WhenCreatingAccount_ShouldFailure(string nameWithNumbers)
  {
    // Arrange
    Result<FirstName> result;
    // Act
    result = FirstName.Create(nameWithNumbers);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(FirstName.Errors.FirstNameCannotContainNumbers(), result.Errors);
  }
  
  [Theory]
  [InlineData("M1l0")]
  [InlineData("R31nh4rd")]
  [InlineData("S3b4st14n")]
  [InlineData("J4k0b")]
  public void GivenLastNameContainingNumbers_WhenCreatingAccount_ShouldFailure(string nameWithNumbers)
  {
    // Arrange
    Result<LastName> result;
    // Act
    result = LastName.Create(nameWithNumbers);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(LastName.Errors.FirstNameCannotContainNumbers(), result.Errors);
  }
  
  // Failure 7
  [Theory]
  [InlineData("M!l#")]
  [InlineData("R$!nh@rd")]
  [InlineData("S!b@st!@n")]
  [InlineData("J@k#b")]
  public void GivenFirstNameContainingSymbols_WhenCreatingAccount_ShouldFailure(string nameWithSymbols)
  {
    // Arrange
    Result<FirstName> result;
    // Act
    result = FirstName.Create(nameWithSymbols);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(FirstName.Errors.FirstNameCannotContainSymbols(), result.Errors);
  }
  
  [Theory]
  [InlineData("M!l#")]
  [InlineData("R$!nh@rd")]
  [InlineData("S!b@st!@n")]
  [InlineData("J@k#b")]
  public void GivenLastNameContainingSymbols_WhenCreatingAccount_ShouldFailure(string nameWithSymbols)
  {
    // Arrange
    Result<LastName> result;
    // Act
    result = LastName.Create(nameWithSymbols);
    // Assert
    Assert.True(result.IsFailure);
    Assert.Contains(LastName.Errors.FirstNameCannotContainSymbols(), result.Errors);
  }
}