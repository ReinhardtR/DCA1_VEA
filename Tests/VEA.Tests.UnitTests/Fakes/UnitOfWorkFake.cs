using VEA.Core.Domain.Common.UnitOfWork;

namespace VEA.Tests.UnitTests.Fakes;

public class UnitOfWorkFake : IUnitOfWork
{
    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}