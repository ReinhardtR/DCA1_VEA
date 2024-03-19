﻿using VEA.Core.Domain.Aggregates.Guests;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Fakes;

public class GuestRepositoryFake : IGuestRepository
{
    //In memory list
    private readonly List<Guest> _guests = [];

    public Task<Result<GuestId>> AddAsync(Guest guest)
    {
        _guests.Add(guest);
        return Task.FromResult(Result.Success(guest.Id));
    }

    public Task<Result<Guest>> FindAsync(GuestId id)
    {
        var guest = _guests.FirstOrDefault(g => g.Id == id);
        return Task.FromResult(guest is null ? Result.Failure<Guest>(Guest.Errors.GuestDoesNotExist()) : Result.Success(guest));
    }

    public Task<Result> RemoveAsync(GuestId id)
    {
        var guest = _guests.FirstOrDefault(g => g.Id == id);
        if (guest is null)
        {
            return Task.FromResult(Result.Failure(Guest.Errors.GuestDoesNotExist()));
        }
        _guests.Remove(guest);
        return Task.FromResult(Result.Success());
    }
}
