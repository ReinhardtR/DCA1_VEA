using VEA.Core.Domain.Common.Values;

namespace VEA.Core.Domain.Aggregates.Guests;

public class Guest
{
    internal GuestId Id;
    internal FirstName FirstName;
    internal LastName LastName;
    internal ViaEmail Email;
    internal GuestProfilePicture ProfilePicture;

    private Guest(GuestId id, FirstName firstName, LastName lastName, ViaEmail email, GuestProfilePicture profilePicture)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ProfilePicture = profilePicture;
    }

    public static Guest Create(GuestId id, FirstName? firstName, LastName? lastName, ViaEmail? email, GuestProfilePicture? profilePicture)
    {
        return new Guest(
            id,
            firstName ?? FirstName.Create("John").Payload,
            lastName ?? LastName.Create("Doe").Payload,
            email ?? ViaEmail.Create("JohnDoe@mail.com").Payload,
            profilePicture ?? GuestProfilePicture.Create(Url.Create("https://www.gravatar.com/avatar/205e460b479e2e5b48aec07710c08d50").Payload).Payload);
    }
    
    
    
}