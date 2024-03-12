using VEA.Core.Domain.Common;

namespace VEA.Tests.UnitTests.Services;

public class StubCurrentTime : ICurrentTime
{
    public DateTime Now()
    {
        return new DateTime(2022, 1, 1);
    }
}