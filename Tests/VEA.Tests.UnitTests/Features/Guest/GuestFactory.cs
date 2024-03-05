
using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Domain.Common.Values;

namespace VEA.Tests.UnitTests.Features.Guest;

public class GuestFactory
{
    private GuestId _id = GuestId.New().Payload;
    private FirstName? _firstName;
    private LastName? _lastName;
    private ViaEmail? _email;
    private GuestProfilePicture? _profilePicture;
    
    public static GuestFactory Create()
    {
        return new GuestFactory();
    }
    
    public GuestFactory WithId(Guid id)
    {
        _id = new GuestId(id);
        return this;
    }
    
    public GuestFactory WithFirstName(string firstName)
    {
        _firstName = FirstName.Create(firstName).Payload;
        return this;
    }
    
    public GuestFactory WithLastName(string lastName)
    {
        _lastName = LastName.Create(lastName).Payload;
        return this;
    }
    
    public GuestFactory WithEmail(string email)
    {
        _email = ViaEmail.Create(email).Payload;
        return this;
    }
    
    public GuestFactory WithProfilePicture(string url)
    {
        _profilePicture = GuestProfilePicture.Create(Url.Create(url).Payload).Payload;
        return this;
    }

    public Core.Domain.Aggregates.Guests.Guest Build()
    {
        return Core.Domain.Aggregates.Guests.Guest.Create(
            _id,
            _firstName,
            _lastName,
            _email,
            _profilePicture
        );
    }
    
}