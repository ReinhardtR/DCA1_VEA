using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public interface IEventRepository
{
    Task<Result> AddAsync(VeaEvent veaEvent);
    Task<Result> FindAsync(EventId id);
    Task<Result> RemoveAsync(EventId id);
}