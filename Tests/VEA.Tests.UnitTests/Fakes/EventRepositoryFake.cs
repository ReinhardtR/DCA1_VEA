using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Fakes;

public class EventRepositoryFake : IEventRepository
{
    // In memory list of events
    private readonly List<VeaEvent> _veaEvents = [];
    
    public Task<Result<EventId>> AddAsync(VeaEvent veaEvent)
    {
        _veaEvents.Add(veaEvent);
        return Task.FromResult(Result.Success(veaEvent.Id));
    }

    public Task<Result<VeaEvent>> FindAsync(EventId id)
    {
        var veaEvent = _veaEvents.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(veaEvent is null ? Result.Failure<VeaEvent>(VeaEvent.Errors.EventDoesNotExist()) : Result.Success(veaEvent));
    }

    public Task<Result> RemoveAsync(EventId id)
    {
        var veaEvent = _veaEvents.FirstOrDefault(e => e.Id == id);
        if (veaEvent is null)
        {
            return Task.FromResult(Result.Failure(VeaEvent.Errors.EventDoesNotExist()));
        }
        _veaEvents.Remove(veaEvent);
        return Task.FromResult(Result.Success());
    }
}