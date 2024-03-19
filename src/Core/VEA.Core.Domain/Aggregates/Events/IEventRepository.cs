using VEA.Core.Tools.OperationResult;

namespace VEA.Core.Domain.Aggregates.Events;

public interface IEventRepository
{
    Task<Result<EventId>> AddAsync(VeaEvent veaEvent);
    Task<Result<VeaEvent>> FindAsync(EventId id);
    Task<Result> RemoveAsync(EventId id);
}