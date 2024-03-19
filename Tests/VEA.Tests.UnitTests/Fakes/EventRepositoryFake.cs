using VEA.Core.Domain.Aggregates.Events;
using VEA.Core.Tools.OperationResult;

namespace VEA.Tests.UnitTests.Fakes;

public class EventRepositoryFake : IEventRepository
{
    // In memory list of events
    private readonly List<VeaEvent> veaEvents = new();
    
    public Task<Result> AddAsync(VeaEvent veaEvent)
    {
        veaEvents.Add(veaEvent);
        return Task.FromResult(Result.Success());
    }

    public Task<Result> FindAsync(EventId id)
    {
        var veaEvent = veaEvents.FirstOrDefault(e => e.Id == id);
        return Task.FromResult(veaEvent is null ? Result.Failure(VeaEvent.Errors.EventDoesNotExist()) : Result.Success());
    }

    public Task<Result> RemoveAsync(EventId id)
    {
        var veaEvent = veaEvents.FirstOrDefault(e => e.Id == id);
        if (veaEvent is null)
        {
            return Task.FromResult(Result.Failure(VeaEvent.Errors.EventDoesNotExist()));
        }
        veaEvents.Remove(veaEvent);
        return Task.FromResult(Result.Success());
    }
}