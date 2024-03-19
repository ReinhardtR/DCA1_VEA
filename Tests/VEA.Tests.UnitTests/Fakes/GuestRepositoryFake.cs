using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Fakes;

public class GuestRepositoryFake : IGuestRepository
{
    //In memory list
    private readonly List<Guest> guests = new();

    public Task<Result> AddAsync(Guest guest)
    {
        guests.Add(guest);
        return Task.FromResult(Result.Success());
    }

    public Task<Result> FindAsync(GuestId id)
    {
        var guest = guests.FirstOrDefault(g => g.Id == id);
        return Task.FromResult(guest is null ? Result.Failure(Guest.Errors.GuestDoesNotExist()) : Result.Success());
    }

    public Task<Result> RemoveAsync(GuestId id)
    {
        var guest = guests.FirstOrDefault(g => g.Id == id);
        if (guest is null)
        {
            return Task.FromResult(Result.Failure(Guest.Errors.GuestDoesNotExist()));
        }
        guests.Remove(guest);
        return Task.FromResult(Result.Success());
    }
}
