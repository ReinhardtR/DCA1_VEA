using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public interface IGuestRepository
{
    Task<Result<GuestId>> AddAsync(Guest guest);
    Task<Result<Guest>> FindAsync(GuestId id);
    Task<Result> RemoveAsync(GuestId id);
}