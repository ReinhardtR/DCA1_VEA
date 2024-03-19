using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Guests;

public interface IGuestRepository
{
    Task<Result> AddAsync(Guest guest);
    Task<Result> FindAsync(GuestId id);
    Task<Result> RemoveAsync(GuestId id);
}